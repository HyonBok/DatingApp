import { Photo } from "./photo";
export interface Produto {
    id: number;
    nome: string;
    preco: number;
    desconto: number;
    marca: string;
    descricao: string;
    sessao: string;
    unidadeVenda: string;
    fotos: Photo[];
    usuario: string;
}