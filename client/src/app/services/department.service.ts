import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { DepartmentResponse } from '../interfaces/department-response';
import { DepartmentCreateRequest } from '../interfaces/department-create-request';
import { DepartmentUpdateRequest } from '../interfaces/department-update-request';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  private apiUrl = environment.employeeServer;
  private http = inject(HttpClient);

  getDepartments(): Observable<DepartmentResponse[]> {
    return this.http.get<DepartmentResponse[]>(
      `${this.apiUrl}/api/v1/departments`
    );
  }

  getDepartment(departmentId: string): Observable<DepartmentResponse> {
    return this.http.get<DepartmentResponse>(
      `${this.apiUrl}/api/v1/departments/${departmentId}`
    );
  }

  createDepartment(dto: DepartmentCreateRequest): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/api/v1/departments`, dto);
  }

  updateDepartment(
    departmentId: string,
    dto: DepartmentUpdateRequest
  ): Observable<void> {
    return this.http.put<void>(
      `${this.apiUrl}/api/v1/departments/${departmentId}`,
      dto
    );
  }

  deleteDepartment(departmentId: string): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/api/v1/departments/${departmentId}`
    );
  }
}
