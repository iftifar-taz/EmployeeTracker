import { Component, inject, Input, input, OnInit, Signal } from '@angular/core';
import { DepartmentService } from '../../services/department.service';
import { DepartmentResponse } from '../../interfaces/department-response';
import { DepartmentStore } from '../../store/department.store';

@Component({
  selector: 'department-grid',
  standalone: true,
  templateUrl: './department-grid.component.html',
  styleUrl: './department-grid.component.css',
})
export class DepartmentGridComponent {
  departmentStore = inject(DepartmentStore);

  rowOnClick(department: DepartmentResponse): void {
    this.departmentStore.selectedDepartment.set(department);
  }
}
