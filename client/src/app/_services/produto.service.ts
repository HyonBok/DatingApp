import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Produto } from '../_models/produto';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {
  baseUrl = environment.apiUrl + 'produto';
  produtos: Produto[] = []

  constructor(private http: HttpClient) { }

  getProdutos(){
    return this.http.get<Produto[]>(this.baseUrl + '/listar');
  }

  getProdutosOferta(){
    return this.http.get<Produto[]>(this.baseUrl + '/listar-ofertas');
  }

  getProduto(id: string){
    return this.http.get<Produto>(this.baseUrl + '/?Id=' + id);
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

  updateProduto(produto: Produto) {
    return this.http.put(this.baseUrl + '/atualizar', produto).pipe(
      map(() => {
        const index = this.produtos.indexOf(produto);
        this.produtos[index] = {...this.produtos[index], ...produto}
      })
    )
  }

  deletarFoto(id: number, fotoId: number){
    return this.http.delete(this.baseUrl + '/deletar-foto/' + id + "/" + fotoId);
  }

  setFoto(id: number, fotoId: number) {
    return this.http.put(this.baseUrl + '/set-foto/' + id + "/" + fotoId, {});
  }
}
