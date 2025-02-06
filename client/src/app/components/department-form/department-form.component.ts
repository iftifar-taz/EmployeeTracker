import { Component, inject, effect, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DepartmentCreateRequest } from '../../interfaces/department-create-request';
import { DepartmentService } from '../../services/department.service';
import { DepartmentResponse } from '../../interfaces/department-response';
import { DepartmentStore } from '../../store/department.store';
import { DepartmentUpdateRequest } from '../../interfaces/department-update-request';

@Component({
  selector: 'department-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './department-form.component.html',
  styleUrl: './department-form.component.css',
})
export class DepartmentFormComponent {
  private fb = inject(FormBuilder);
  private departmentService = inject(DepartmentService);
  departmentStore = inject(DepartmentStore);

  departmentForm = this.fb.group({
    name: ['', [Validators.required]],
    key: ['', [Validators.required]],
    description: [''],
  });

  constructor() {
    effect(() => {
      const selectedDepartment = this.departmentStore.selectedDepartment();
      this.departmentForm.setValue({
        name: selectedDepartment?.departmentName ?? '',
        key: selectedDepartment?.departmentKey ?? '',
        description: selectedDepartment?.description ?? '',
      });
    });
  }

  onSubmit() {
    if (this.departmentForm.valid) {
      !!this.departmentStore.selectedDepartment()
        ? this.updateDepartment()
        : this.createDepartment();
    }
  }

  private createDepartment(): void {
    const dto: DepartmentCreateRequest = {
      departmentName: this.departmentForm.value.name!,
      departmentKey: this.departmentForm.value.key!,
      description: this.departmentForm.value.description!,
    };
    this.departmentService.createDepartment(dto).subscribe((res: string) => {
      const newDepartment: DepartmentResponse = {
        departmentId: res,
        departmentName: this.departmentForm.value.name!,
        departmentKey: this.departmentForm.value.key!,
        description: this.departmentForm.value.description!,
        employeeCount: 0,
      };
      this.departmentStore.departmentList.set([
        ...this.departmentStore.departmentList(),
        newDepartment,
      ]);
      this.departmentForm.reset();
    });
  }

  private updateDepartment(): void {
    const dto: DepartmentUpdateRequest = {
      departmentId: this.departmentStore.selectedDepartment()?.departmentId!,
      departmentName: this.departmentForm.value.name!,
      departmentKey: this.departmentForm.value.key!,
      description: this.departmentForm.value.description!,
    };
    this.departmentService
      .updateDepartment(dto.departmentId, dto)
      .subscribe(() => {
        const list = this.departmentStore.departmentList();
        const item = list.find((x) => x.departmentId === dto.departmentId)!;
        item.departmentName = dto.departmentName;
        item.departmentKey = dto.departmentKey;
        item.description = dto.description;
        this.departmentStore.departmentList.set(list);
        this.departmentStore.selectedDepartment.set(null);
        this.departmentForm.reset();
      });
  }
}
