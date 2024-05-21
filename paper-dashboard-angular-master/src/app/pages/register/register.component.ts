import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SignUpRequest } from 'app/models/SignUpReqquest';
import { UsersService } from 'app/services/users.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  signUp : SignUpRequest;
  constructor(
    private formBuilder: FormBuilder,
    private userService: UsersService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      name: ['', Validators.required],
      lastName: ['', Validators.required],
      date: ['', Validators.required],
      cin: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      agency: ['', Validators.required],
      address: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      role: ['client'],
      accountNumber: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.signUp = {
      IdRef:this.registerForm.get('accountNumber').value,
      Name:this.registerForm.get('name').value,
      LastName:this.registerForm.get('lastName').value,
      Date: this.registerForm.get('date').value,
      CIN:this.registerForm.get('cin').value,
      PhoneNumber:this.registerForm.get('phoneNumber').value,
      agency:this.registerForm.get('agency').value,
      adsress:this.registerForm.get('address').value,
      email:this.registerForm.get('email').value,
      role:'client',
      AccountNumber:this.registerForm.get('accountNumber').value,
      password:this.registerForm.get('password').value
    };
    
    console.log(this.signUp);
    if (this.registerForm.valid) {
      this.userService.register(this.signUp).subscribe((result) => {
        console.log(result);
        this.router.navigate(['/login']);
      })
    }
  }

}
