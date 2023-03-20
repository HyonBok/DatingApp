import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Produto } from 'src/app/_models/produto';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { ProdutoService } from 'src/app/_services/produto.service';

@Component({
  selector: 'app-produto-listself',
  templateUrl: './produto-listself.component.html',
  styleUrls: ['./produto-listself.component.css']
})
export class ProdutoListselfComponent implements OnInit {
  produtos: Produto[] = [];
  
  user: User | null = null;
  member: Member | undefined;

  i: number = 0;

  constructor(private accountService: AccountService, private produtoService: ProdutoService,
    private memberService: MembersService, private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.user = user
    })
   }

  ngOnInit(): void {
    this.loadMember();

    this.loadProdutos();
  }

  loadMember(){
    if(!this.user) return;
    this.memberService.getMember(this.user.username).subscribe({
      next: member => this.member = member
    })
  }

  loadProdutos(){
    if(!this.member) return;
    this.produtoService.getProdutosByName(this.member.userName);
  }
}

