import { Component, EventEmitter, OnInit, Input, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
      confirmPassword: new FormControl('', Validators.required)
    }, this.passwordMatchValidator);
  }

  passwordMatchValidator(abcdef: FormGroup) {
    return abcdef.get('password').value === abcdef.get('confirmPassword').value ? null : {'mismatch': true};
  }

  register() {
    // this.authService.register(this.model).subscribe(() => {
    //   /*console.log('Registration successful');*/
    //   this.alertify.success('Registration was a success');
    // }, error => {
    //   /*console.log(error);*/
    //   this.alertify.error(error);
    // });
    console.log(this.registerForm.value);
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
