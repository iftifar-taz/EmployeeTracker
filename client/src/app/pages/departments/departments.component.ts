import { Component, inject, OnInit, signal } from '@angular/core';
import { DepartmentGridComponent } from '../../components/department-grid/department-grid.component';
import { DepartmentFormComponent } from '../../components/department-form/department-form.component';
import { DepartmentService } from '../../services/department.service';
import { DepartmentResponse } from '../../interfaces/department-response';
import { DepartmentStore } from '../../store/department.store';

@Component({
  selector: 'app-departments',
  standalone: true,
  imports: [DepartmentGridComponent, DepartmentFormComponent],
  templateUrl: './departments.component.html',
  styleUrl: './departments.component.css',
})
export class DepartmentsComponent implements OnInit {
  private departmentService = inject(DepartmentService);
  private departmentStore = inject(DepartmentStore);

  ngOnInit(): void {
    this.departmentService.getDepartments().subscribe((res) => {
      this.departmentStore.departmentList.set(res);
    });
  }
}
