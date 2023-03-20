import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { ProdutoService } from '../../_services/produto.service';

@Component({
  selector: 'app-vender',
  templateUrl: './vender.component.html',
  styleUrls: ['./vender.component.css']
})
export class VenderComponent implements OnInit {
  produto: any = {};
  user: User | null = null;
  member: Member | undefined;

  constructor(private accountService: AccountService, private produtoService: ProdutoService, 
    private memberService: MembersService, private toastr: ToastrService) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe({
        next: user => this.user = user
      })
    }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    if(!this.user) return;
    this.memberService.getMember(this.user.username).subscribe({
      next: member => this.member = member
    })
  }

  vender(){
    if(!this.produto) return;
    if(!this.member) return;
    this.produto.userName = this.member.userName;
    this.produtoService.registrarProduto(this.produto).subscribe({
      error: error => this.toastr.error(error.error)
    })
  }
}
