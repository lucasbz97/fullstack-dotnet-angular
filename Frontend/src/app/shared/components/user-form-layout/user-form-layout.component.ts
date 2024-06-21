import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, ReactiveFormsModule } from '@angular/forms';
import { UserModel } from '../../../core/models/user.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgIf, NgFor } from '@angular/common';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './user-form-layout.component.html',
  styleUrls: ['./user-form-layout.component.scss']
})
export class UserFormComponent implements OnInit, OnChanges {
  @Input() user: UserModel | null = null;
  @Input() title: string = '';
  @Output() formSubmit = new EventEmitter<UserModel>();
  @Output() formCancel = new EventEmitter<void>();

  userForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.userForm = this.fb.group({
      name: ['', [Validators.required]],
      age: [null, [Validators.required]],
      dependents: this.fb.array([])
    });
  }

  ngOnInit(): void {
    if (this.user) {
      this.populateForm(this.user);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['user'] && this.user) {
      this.populateForm(this.user);
    }
  }

  populateForm(user: UserModel): void {
    this.userForm.patchValue(user);
    this.dependents.clear();
    user.dependents.forEach(dependent => {
      this.dependents.push(this.createDependentGroup(dependent.id, dependent.name, dependent.age));
    });
  }

  get dependents(): FormArray {
    return this.userForm.get('dependents') as FormArray;
  }

  createDependentGroup(id: number | null = null, name: string = '', age: number = 0): FormGroup {
    return this.fb.group({
      id: [{ value: id, disabled: true }],
      name: [name, [Validators.required]],
      age: [age, [Validators.required, Validators.min(19), Validators.max(120)]]
    });
  }

  addDependent(): void {
    this.dependents.push(this.createDependentGroup());
  }

  removeDependent(index: number): void {
    this.dependents.removeAt(index);
  }

  onSubmit(): void {
    if (this.userForm.valid) {
      this.dependents.controls.forEach(group => group.get('id')?.enable());
      this.formSubmit.emit(this.userForm.value);
    }
  }

  onCancel(): void {
    this.formCancel.emit();
  }
}
