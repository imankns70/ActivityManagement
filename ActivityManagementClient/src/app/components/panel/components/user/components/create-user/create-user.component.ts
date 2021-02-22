import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/components/auth/services/auth.service';
import { User } from 'src/app/models/user/user';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { UserGridService } from '../../services/User.Grid.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit {


  useForm: FormGroup;

  @Input() public isActiveForm = false;
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<User> = new EventEmitter();


  constructor(private formBuilder: FormBuilder, private authService: AuthService,
    private userGridService: UserGridService, private alertService: NotificationMessageService) { }
  ngOnInit() {

    this.useForm = this.formBuilder.group({

      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4)]],
      gender: ['', Validators.required],

    }, [this.checkPasswordMatch, this.checkAcceptcondition])

  }
 

get passwordField() {return this.useForm.get('password');}
get emailField() { return this.useForm.get('email');}

  checkPasswordMatch(g: FormGroup) {
    return g.get('password').value === g.get('confirmpassword').value ? null : { mismath: true }
  }
  checkAcceptcondition(g: FormGroup) {

    return g.get('acceptcondition').value === true ? null : { acceptmismath: true }
  }

  onSave(e) {
  
    this.save.emit(this.useForm.value);
    this.isActiveForm = false;

  }

  closeForm(e) {

    this.isActiveForm = false;
    this.cancel.emit();
  }
}