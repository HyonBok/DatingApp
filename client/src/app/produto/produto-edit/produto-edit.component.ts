import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Produto } from 'src/app/_models/produto';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { ProdutoService } from 'src/app/_services/produto.service';

@Component({
  selector: 'app-produto-edit',
  templateUrl: './produto-edit.component.html',
  styleUrls: ['./produto-edit.component.css']
})
export class ProdutoEditComponent implements OnInit {

  // @ViewChild('editForm') editForm: NgForm | undefined;
  // @HostListener('window:beforeunload', ['$event']) unloadNotificaton($event:any){
  //   if(this.editForm?.dirty){
  //     $event.returnValue = true;
  //   }
  // }
  user: User | null = null;
  produto: Produto | undefined;

  constructor(private accountService: AccountService, 
    private produtoService: ProdutoService, private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.user = user
    })
   }

  ngOnInit(): void {
    this.loadProduto();
  }

  loadProduto(){
    if(!this.user) return;
    this.produtoService.getProduto(this.user.username).subscribe({
      next: produto => this.produto = produto
    })
  }

  // updateMember(){
  //   this.memberService.updateMember(this.editForm?.value).subscribe({
  //     next: _ => {
  //       this.toastr.success('Perfil atualizado');
  //       this.editForm?.reset(this.member);
  //     }
  //   })
  // }

}
