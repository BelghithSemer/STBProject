import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from 'app/models/LoginRequest';
import { users } from 'app/models/users';
import { UsersService } from 'app/services/users.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginform: FormGroup;
  loginrequest : LoginRequest;
  user : users;
  constructor(
    private formBuilder: FormBuilder,
    private userService: UsersService,
    private router: Router
  ) {
    this.loginform = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    sessionStorage.removeItem('user');
  }
 

  onSubmit(): void {
    
    this.loginrequest = {
      Id: 12,
      Email: this.loginform.get('email').value,
      Password : this.loginform.get('password').value
    }
    this.userService.login(this.loginrequest).subscribe((result)=>{
      sessionStorage.setItem('user',JSON.stringify(result));
      this.user = JSON.parse(sessionStorage.getItem('user'));
      console.log(this.user);
      if(this.user.role == 'client'){
        this.router.navigate(['/dashboard']);
      }else{
        this.router.navigate(['/request']);
      }
    })

  }

}
