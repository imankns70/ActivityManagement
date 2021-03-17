import { Component, EventEmitter, Input, OnInit, Output, } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/models/user/user';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {

  useForm: FormGroup;
  @Input() public isActiveEditForm = false;
  @Input() public set model(user: User) {
   debugger;
    if (typeof user !== 'undefined') {
      this.useForm.reset(user)
    }
  };
  @Output() save: EventEmitter<FormGroup> = new EventEmitter();
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {

    this.useForm = this.formBuilder.group({
      id: [''],
      roleId: [''],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      username: ['', Validators.required],
      persianBirthDate: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(10)]],
      gender: ['', Validators.required],

    }, [this.checkPasswordMatch, this.checkAcceptcondition])
  }
  get passwordField() { return this.useForm.get('password'); }
  get emailField() { return this.useForm.get('email'); }
  get genderField() { return this.useForm.get('gender'); }

  checkPasswordMatch(g: FormGroup) {
    return g.get('password').value === g.get('confirmpassword').value ? null : { mismath: true }
  }
  checkAcceptcondition(g: FormGroup) {

    return g.get('acceptcondition').value === true ? null : { acceptmismath: true }
  }
  onSave(e) {
    e.preventDefault();
    this.save.emit(this.useForm);
    //this.isActiveForm = false;

  }

  closeForm(e) {
    e.preventDefault();
    this.isActiveEditForm = false;
    //this.cancel.emit();
  }
}
