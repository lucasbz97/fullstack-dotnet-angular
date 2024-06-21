import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { UserModel } from '../../core/models/user.model';
import { NgFor } from '@angular/common';
import { TableInformationComponent } from './features/table-information/table-information.component';



@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    TableInformationComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  
}
