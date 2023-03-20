import { Component, Input, OnInit } from '@angular/core';
import { Produto } from '../../_models/produto';

@Component({
  selector: 'app-produto-card',
  templateUrl: './produto-card.component.html',
  styleUrls: ['./produto-card.component.css']
})
export class ProdutoCardComponent implements OnInit {
  @Input() produto: Produto | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
