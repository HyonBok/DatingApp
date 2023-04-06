import { Foto } from "./foto";
export interface Produto {
    id: number;
    nome: string;
    preco: number;
    desconto: number;
    precoDesconto: string;
    marca: string;
    descricao: string;
    sessao: string;
    unidadeVenda: string;
    fotos: Foto[];
    fotoUrl: string;
    usuario: string;
}