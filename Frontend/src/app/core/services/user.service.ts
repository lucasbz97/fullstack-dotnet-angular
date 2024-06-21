import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserModel } from '../models/user.model';
import { environment } from '../../../env/env';
import { CreateUserRequest } from '../request/create-user-request';
import { UpdateUserRequest } from '../request/update-user-request';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsers(): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(`${this.apiUrl}/api/user`);
  }

  addUser(request: CreateUserRequest): Observable<UserModel> {
    return this.http.post<UserModel>(`${this.apiUrl}/api/user`, request);
  }

  deleteUser(userId: number): Observable<void> {
    const url = `${this.apiUrl}/api/user/${userId}`;
    return this.http.delete<void>(url);
  }

  updateUser(updatedUser: UpdateUserRequest): Observable<UserModel> {
    const url = `${this.apiUrl}/api/user/${updatedUser.id}`;
    return this.http.put<UserModel>(url, updatedUser);
  }
}
