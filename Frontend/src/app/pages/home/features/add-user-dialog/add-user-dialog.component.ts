import { Component } from '@angular/core';
import { MatDialogActions, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormArray } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { UserService } from '../../../../core/services/user.service';
import { CreateUserRequest } from '../../../../core/request/create-user-request';
import { CreateDependentRequest } from '../../../../core/request/create-dependent-request';
import { UserFormComponent } from '../../../../shared/components/user-form-layout/user-form-layout.component';
import { catchError, of, tap } from 'rxjs';

@Component({
  selector: 'app-add-user-dialog',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    MatIconModule,
    ReactiveFormsModule, 
    UserFormComponent,
    MatFormFieldModule,
    MatDialogActions,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './add-user-dialog.component.html',
  styleUrls: ['./add-user-dialog.component.scss']
})
export class AddUserDialogComponent {
  
  constructor(
    public dialogRef: MatDialogRef<AddUserDialogComponent>,
    private userService: UserService
  ) { }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(user: any): void {
    let dependentRequest: CreateDependentRequest = {
      dependentsData: []
    }
    if (user.dependents.length > 0) {
      user.dependents.forEach((element: any) => {
        dependentRequest.dependentsData?.push(element);
      });
    }

    const request: CreateUserRequest = {
      age: user.age,
      name: user.name,
      dependent: dependentRequest
    }

    this.userService.addUser(request).pipe(
      tap((response: any) => {
        console.log('Usuário adicionado com sucesso:', response);
        this.dialogRef.close(response);
      }),
      catchError((error) => {
        console.error('Erro ao adicionar usuário:', error);
        return of(null);
      })
    ).subscribe();
  }
}
