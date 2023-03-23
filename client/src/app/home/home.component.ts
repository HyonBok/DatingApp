import { Component, OnInit } from '@angular/core';
import { Produto } from '../_models/produto';
import { AccountService } from '../_services/account.service';
import { ProdutoService } from '../_services/produto.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  loginMode = false;
  users: any;
  produtos: Produto[] = [];

  constructor(private accountService: AccountService, private produtoService: ProdutoService) { }

  ngOnInit(): void {
    //this.carregarProdutos();
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

  carregarProdutos(){
    this.produtoService.getProdutos().subscribe({
      next: produtos => this.produtos = produtos
    })
  }
}
