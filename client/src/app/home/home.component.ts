import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
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
  logado = false;
  users: any;
  produtosOferta: Produto[] = [];

  constructor(private accountService: AccountService, private produtoService: ProdutoService) { }

  ngOnInit(): void {
    // Consegue saber se o usuário está logado atravess do 'currentUser$' do accountService
    this.loadUser();

    this.loadProdutos();
  }

  loadUser(){
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if(user) {
          this.logado = true;
        }
      }
    })
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

  loadProdutos(){
    this.produtoService.getProdutos().subscribe({
      next: produtosOferta => this.produtosOferta = produtosOferta
    })
  }
}
