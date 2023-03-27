import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
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
    this.loadProdutos();
  }

  // Função para carregar os produtos, usar sempre que tiver lista de produtos e precisar de possiveis alterações (excluir, mudar página, etc).
  loadProdutos(){
    this.produtoService.getProdutos().subscribe({
      next: produtos => {
        this.produtos = produtos
      },
      error: (error) => {
        console.log(error);
      }
    })
  }
}
