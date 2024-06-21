import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../../../core/models/user.model';
import { NgFor } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { UserService } from '../../../../core/services/user.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { AddUserDialogComponent } from '../add-user-dialog/add-user-dialog.component';
import { EditUserDialogComponent } from '../edit-user-dialog/edit-user-dialog.component';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { CreateUserRequest } from '../../../../core/request/create-user-request';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-table-information',
  standalone: true,
  imports: [
    NgFor,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  templateUrl: './table-information.component.html',
  styleUrl: './table-information.component.scss'
})
export class TableInformationComponent implements OnInit {
  displayedColumns: string[] = ['Posicao', 'Nome', 'Idade', 'Dependentes', 'actions'];
  users: UserModel[] = [];
  constructor(private userService: UserService, private dialog: MatDialog){}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getUsers().pipe(
      tap((users: any) => {
        this.users = users.data;
      }),
      catchError((error) => {
        console.error('Erro ao carregar usuários:', error);
        return new Observable<UserModel[]>();
      })
    ).subscribe();
  }

  openAddUserDialog(): void {
    const dialogRef = this.dialog.open(AddUserDialogComponent, {
      width: '800px',
      height: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadUsers();
      }
    });
  }
  
  openEditUserDialog(user: UserModel): void {
    const dialogRef = this.dialog.open(EditUserDialogComponent, {
      data: user,
      width: '800px',
      height: '600px'
    });

    dialogRef.afterClosed().pipe(
      tap((result) => {
        if (result) {
          this.loadUsers();
        }
      })
    ).subscribe();
  }

  deleteUser(userId: number): void {
    this.userService.deleteUser(userId).pipe(
      tap(() => {
        console.log(`Usuário com ID ${userId} deletado com sucesso.`);
        this.loadUsers();
      }),
      catchError((error) => {
        console.error(`Erro ao deletar usuário com ID ${userId}:`, error);
        return new Observable();
      })
    ).subscribe();
  }
}
