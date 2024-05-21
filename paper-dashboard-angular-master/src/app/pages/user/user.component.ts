import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { users } from 'app/models/users';
import { UsersService } from 'app/services/users.service';

@Component({
    selector: 'user-cmp',
    moduleId: module.id,
    templateUrl: 'user.component.html'
})

export class UserComponent implements OnInit {
    user : users;
    updateForm : FormGroup;
    constructor(private userService: UsersService, fb:FormBuilder, private router: Router) {
        this.updateForm = fb.group({
            name: ['', Validators.required],
            lastName: ['', Validators.required],
            date: ['', Validators.required],
            cin: ['', Validators.required],
            phoneNumber: ['', Validators.required],
            agency: ['', Validators.required],
            address: ['', Validators.required],
            accountNumber: ['', Validators.required]
        });
    }
    ngOnInit() {
        this.user = JSON.parse(sessionStorage.getItem('user'));
        console.log(this.user.id);
        this.updateForm.patchValue(this.user);
    }

    onSubmit(){
        this.user.name = this.updateForm.get('name').value;
        this.user.lastName = this.updateForm.get('lastName').value;
        this.user.date = this.updateForm.get('date').value;
        this.user.cin = this.updateForm.get('cin').value;
        this.user.phoneNumber = this.updateForm.get('phoneNumber').value;
        this.user.agency = this.updateForm.get('agency').value;
        this.user.accountNumber = this.updateForm.get('accountNumber').value;
        this.user.adsress = this.updateForm.get('address').value;

        sessionStorage.setItem('user',JSON.stringify(this.user));

        this.userService.Update(this.user).subscribe(()=>{
            this.router.navigate(['/user']);
        })
    }
    
}
