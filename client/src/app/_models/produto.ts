import { Member } from "./member";
export interface Produto {
    id: number;
    nome: string;
    preco: number;
    precopromocao: number;
    marca: string;
    descricao: string;
    unidadeVenda: string;
    foto: string;
    userName: string;
}