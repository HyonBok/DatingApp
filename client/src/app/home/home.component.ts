import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  loginMode = false;
  users: any;

  constructor(private http: HttpClient, private accountService: AccountService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle(){
    this.accountService.logout();
    this.registerMode = !this.registerMode;
  }

  loginToggle(){
    this.accountService.logout();
    this.loginMode = !this.loginMode;
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,
      error: error  => console.log(error),
      complete: () => console.log('Request has completed')
    })
  }

  cancelRegisterMode(event:boolean) {
    this.registerMode = event;
  }

  cancelLoginMode(event:boolean) {
    this.loginMode = event;
  }
}
