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

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  registerToggle(){
    this.accountService.logout();
    this.registerMode = !this.registerMode;
  }

  loginToggle(){
    this.accountService.logout();
    this.loginMode = !this.loginMode;
  }

  cancelRegisterMode(event:boolean) {
    this.registerMode = event;
  }

  cancelLoginMode(event:boolean) {
    this.loginMode = event;
  }
}
