import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { FormControl, NgForm, NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { ToastrService } from 'ngx-toastr';
import { Produto } from 'src/app/_models/produto';
import { User } from 'src/app/_models/user';
import { ProdutoService } from 'src/app/_services/produto.service';

@Component({
  selector: 'app-produto-edit',
  templateUrl: './produto-edit.component.html',
  styleUrls: ['./produto-edit.component.css']
})
export class ProdutoEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm | undefined;
  @HostListener('window:beforeunload', ['$event']) unloadNotificaton($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  user: User | null = null;
  produto: Produto | undefined;
  galleryOption: NgxGalleryOptions | undefined;
  galleryImage: NgxGalleryImage | undefined;

  constructor(private produtoService: ProdutoService, private toastr: ToastrService,
    private route: ActivatedRoute, private router: Router) {
    this.galleryOption = {
      width: '500px',
      height: '500px',
      imagePercent: 100,
      thumbnailsColumns: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview: false
    }
  }

  ngOnInit(): void {
    this.loadProduto();
  }

  getImages() {
    if (!this.produto) return;
    const imageUrls = ({
      small: this.produto.fotos,
      medium: this.produto.fotos,
      big: this.produto.fotos,
    })
    return imageUrls;
  }

  loadProduto() {
    const produto = this.route.snapshot.paramMap.get('id');
    if (!produto) return;
    this.produtoService.getProduto(produto).subscribe({
      next: produto => {
        this.produto = produto,
        this.galleryImage = this.getImages()
      }
    })
  }


  updateProduto() {
    this.editForm?.form.addControl('id', new FormControl(''));
    this.editForm?.controls['id'].setValue(this.produto?.id);
    this.produtoService.updateProduto(this.editForm?.value).subscribe({
      next: _ => {
        this.toastr.success('Produto atualizado');
        this.editForm?.reset(this.produto);
      },
      error: _ => {
        console.log("a");
      }
    })
  }

  deleteProduto() {
    if(!this.produto) return;
    this.produtoService.deleteProduto(this.produto.id).subscribe({
      next: () => {
        this.router.navigateByUrl('/produtos');
        this.toastr.success("Deletado com sucesso.");
      },
      error: () => {
        this.toastr.error("Falha ao deletar.")
      }
    })
  }

  
}
