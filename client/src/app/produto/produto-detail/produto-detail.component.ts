import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryImage, NgxGalleryOptions, NgxGalleryAnimation } from '@kolkov/ngx-gallery';
import { ToastrService } from 'ngx-toastr';
import { Produto } from 'src/app/_models/produto';
import { ProdutoService } from 'src/app/_services/produto.service';

@Component({
  selector: 'app-produtos-detail',
  templateUrl: './produto-detail.component.html',
  styleUrls: ['./produto-detail.component.css']
})
export class ProdutoDetailComponent implements OnInit {
  produto: Produto | undefined;
  galleryOption: NgxGalleryOptions | undefined;
  galleryImage: NgxGalleryImage | undefined;

  constructor(private produtoService: ProdutoService, private route: ActivatedRoute,
    private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.loadProduto();

    this.galleryOption = {
      width: '500px',
      height: '500px',
      imagePercent: 100,
      thumbnailsColumns: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview: false
    }
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

  loadProduto(){
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;
    this.produtoService.getProduto(id).subscribe({
      next: produto => {
        this.produto = produto,
        this.galleryImage = this.getImages()
      }
    })
  }

  editProduto() {
    if(!this.produto) return;
    this.produtoService.deleteProduto(this.produto.id).subscribe({
      next: () => {
        this.router.navigateByUrl('/produto/edit/' + this.produto?.id);
      },
      error: () => {
        this.toastr.error("Falha ao deletar.")
      }
    })
  }
}
