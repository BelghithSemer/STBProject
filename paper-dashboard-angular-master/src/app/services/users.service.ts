import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from 'app/models/LoginRequest';
import { SignUpRequest } from 'app/models/SignUpReqquest';
import { users } from 'app/models/users';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  
  constructor(private http: HttpClient) { }

  public getData = (accountNumber:string) => {
     return this.http.get<users>(`https://localhost:7025/api/User/account/${accountNumber}`);
  }
  
  public register(user : SignUpRequest){
    return this.http.post('https://localhost:7025/api/Auth/SignUp',user);
  }

  public login(request : LoginRequest){
    return this.http.post('https://localhost:7025/api/Auth/login',request)
  }

  public Update(user: users){
    console.log('updating user profile')
    console.log(user);
    return this.http.put(`https://localhost:7025/api/User/${user.id}`,user)
  }
   
  
}