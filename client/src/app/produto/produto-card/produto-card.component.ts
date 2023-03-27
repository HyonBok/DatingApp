import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { ProdutoService } from 'src/app/_services/produto.service';
import { Produto } from '../../_models/produto';

@Component({
  selector: 'app-produto-card',
  templateUrl: './produto-card.component.html',
  styleUrls: ['./produto-card.component.css']
})
export class ProdutoCardComponent implements OnInit {
  @Output() deleteClicked = new EventEmitter<void>();
  @Input() produto: Produto | undefined;
  user: User | undefined;
  member: Member | undefined;
  perm = false;

  constructor(private accountService: AccountService, private toastr: ToastrService,
    private produtoService: ProdutoService, private memberService: MembersService) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe({
        next: user => {
          if(user)
            this.user = user
        }
      })
  }

  ngOnInit(): void {
    this.loadMember();

    this.Permissao();
  }

  loadMember() {
    if(!this.user) return
    this.memberService.getMember(this.user.username).subscribe({
      next: member => this.member = member
    })
  }

  Permissao() {
    if(this.produto && this.user && this.produto.usuario == this.user.username){
      this.perm = true;
    }
  }

  deleteProduto() {
    if(!this.produto) return;
    this.produtoService.deleteProduto(this.produto.id).subscribe({
      next: () => {
        this.deleteClicked.emit();
        this.toastr.success("Deletado com sucesso.");
      },
      error: () => {
        this.toastr.error("Falha ao deletar.")
      }
    })
  }
}

