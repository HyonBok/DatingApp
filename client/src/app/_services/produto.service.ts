import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Produto } from '../_models/produto';
import { MembersService } from './members.service';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {
  baseUrl = environment.apiUrl + 'produto';

  constructor(private http: HttpClient) { }

  getProdutos(){
    return this.http.get<Produto[]>(this.baseUrl + '/listar');
  }

  getProdutosOferta(){
    return this.http.get<Produto[]>(this.baseUrl + '/listarOfertas');
  }

  getProduto(nome: string){
    return this.http.get<Produto>(this.baseUrl + '/' + nome);
  }

  registrarProduto(produto: Produto){
    return this.http.post<Produto>(this.baseUrl + '/registrar', produto);
  }

  getProdutosByName(nome:string){
    return this.http.get<Produto[]>(this.baseUrl + '/listself/' + nome);
  }

  deleteProduto(id:number){
    return this.http.delete(this.baseUrl + '/deletar/' + id);
  }
}
