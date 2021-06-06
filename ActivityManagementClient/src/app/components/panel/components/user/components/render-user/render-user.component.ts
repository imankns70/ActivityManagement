import { Component, EventEmitter, Input, OnInit, Output, } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { gender } from 'src/app/models/enums/gender';
import { User } from 'src/app/models/user/user';
import { RouterStateUrl } from 'src/app/store/_model/routerStateUrl';
import * as fromLocalStore from '../../store'

@Component({
  selector: 'app-render-user',
  templateUrl: './render-user.component.html',
  styleUrls: ['./render-user.component.scss']
})
export class RenderUserComponent implements OnInit {

  userId: number;
 

  useForm: FormGroup;

  public active = false;

  @Input() public isNew = false;
  @Input() public set model(user: User) {


    if (typeof user !== "undefined") {

 
      this.useForm.reset({
   
        id: user.id,
        firstName: user.firstName,
        lastName: user.lastName,
        userName: user.userName,
        email: user.email,
        persianBirthDate: user.persianBirthDate,
        gender: user.gender == gender.male ? "male" : "female"
      });

      this.active = true;

    }


  };
  @Output() save: EventEmitter<User> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  modalTitle: string;

  constructor(private formBuilder: FormBuilder,private store: Store<RouterStateUrl>) { }

  ngOnInit() {

    this.store.select(fromLocalStore.getRouterUserId).subscribe(console.log)
    this.modalTitle = this.isNew ? 'ویرایش کاربر' : 'ایجادکاربر';

    this.useForm = this.formBuilder.group({
      id: [''],
      roleId: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userName: ['', Validators.required],
      persianBirthDate: [''],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(10)]],
      gender: ['', Validators.required],

    }, [this.checkPasswordMatch, this.checkAcceptcondition]);

    console.log(this.useForm);
  }
  get passwordField() { return this.useForm.get('password'); }
  get emailField() { return this.useForm.get('email'); }

  checkPasswordMatch(g: FormGroup) {
    return g.get('password').value === g.get('confirmpassword').value ? null : { mismath: true }
  }
  checkAcceptcondition(g: FormGroup) {

    return g.get('acceptcondition').value === true ? null : { acceptmismath: true }
  }
  onSave(e) {
    //e.preventDefault();
    this.save.emit(this.useForm.value);
    this.active = false;
    //this.isActiveForm = false;

  }

  closeForm() {
    //e.preventDefault();
    this.active = false;
    this.cancel.emit();
  }
}
