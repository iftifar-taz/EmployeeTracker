import { Injectable, signal } from '@angular/core';
import { DepartmentResponse } from '../interfaces/department-response';

@Injectable({
  providedIn: 'root',
})
export class DepartmentStore {
  departmentList = signal<DepartmentResponse[]>([]);
  selectedDepartment = signal<DepartmentResponse | null>(null);
}
