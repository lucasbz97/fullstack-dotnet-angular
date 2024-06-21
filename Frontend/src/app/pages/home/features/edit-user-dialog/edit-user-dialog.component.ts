import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormArray } from '@angular/forms';
import { UserModel } from '../../../../core/models/user.model';
import { NgFor, NgIf } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { UserService } from '../../../../core/services/user.service';
import { catchError, of, tap } from 'rxjs';
import { UpdateDependentRequest } from '../../../../core/request/update-dependent-request';
import { UpdateUserRequest } from '../../../../core/request/update-user-request';
import { UserFormComponent } from '../../../../shared/components/user-form-layout/user-form-layout.component';

@Component({
  selector: 'app-edit-user-dialog',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    UserFormComponent,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './edit-user-dialog.component.html',
  styleUrls: ['./edit-user-dialog.component.scss']
})
export class EditUserDialogComponent {
  user: UserModel;

  constructor(
    public dialogRef: MatDialogRef<EditUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserModel,
    private userService: UserService
  ) { this.user = data; }

  onSubmit(user: any): void {
    let dependentRequest: UpdateDependentRequest[] = [];
    if (user.dependents.length > 0) {
      user.dependents.forEach((element: any) => {
        dependentRequest.push({
          id: element.id,
          age: element.age,
          name: element.name,
          userId: this.user.id
        });
      });
    }

    const request: UpdateUserRequest = {
      id: this.user.id,
      age: user.age,
      name: user.name,
      dependents: dependentRequest
    }

    this.userService.updateUser(request).pipe(
      tap((response: any) => {
        console.log('Usuário atualizado com sucesso:', response);
        this.dialogRef.close(response);
      }),
      catchError((error) => {
        console.error('Erro ao atualizar usuário:', error);
        return of(null);
      })
    ).subscribe();
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
