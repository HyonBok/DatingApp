import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Produto } from '../_models/produto';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getProdutos(){
    return this.http.get<Produto[]>(this.baseUrl + 'produto/listar');
  }

  getProduto(nome: string){
    return this.http.get<Produto>(this.baseUrl + 'produto/' + nome);
  }

  registrarProduto(produto: Produto){
    return this.http.post<Produto>(this.baseUrl + 'produto/registrar', produto);
  }
}
