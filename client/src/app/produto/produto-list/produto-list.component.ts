import { Component, OnInit } from '@angular/core';
import { Produto } from 'src/app/_models/produto';
import { ProdutoService } from 'src/app/_services/produto.service';

@Component({
  selector: 'app-produtos',
  templateUrl: './produto-list.component.html',
  styleUrls: ['./produto-list.component.css']
})
export class ProdutoListComponent implements OnInit {
  produtos: Produto[] = []

  constructor(private produtoService: ProdutoService) { }

  ngOnInit(): void {
    this.carregarProdutos();
  }

  carregarProdutos(){
    this.produtoService.getProdutos().subscribe({
      next: produtos => this.produtos = produtos
    })
  }
}
