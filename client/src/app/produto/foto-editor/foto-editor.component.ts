import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Produto } from 'src/app/_models/produto';
import { Foto } from 'src/app/_models/foto';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { ProdutoService } from 'src/app/_services/produto.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-foto-editor',
  templateUrl: './foto-editor.component.html',
  styleUrls: ['./foto-editor.component.css']
})
export class FotoEditorComponent implements OnInit {
  @Input() produto: Produto | undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User | undefined;

  constructor(private accountService: AccountService,
    private produtoService: ProdutoService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if(user) this.user = user
      }
    })
  }

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any){
    this.hasBaseDropZoneOver = e;
  }

  setFoto(foto: Foto) {
    if(!this.produto) return;
    this.produtoService.setFoto(this.produto.id, foto.id).subscribe({
      next: () => {
        if(this.produto) {
          this.produto.fotoUrl = foto.url;
          this.produto.fotos.forEach(p => {
            if(p.isMain) p.isMain = false;
            if(p.id == foto.id) p.isMain = true;
          })
        }
      }
    })
  }

  deletarFoto(fotoId: number){
    if(!this.produto) return;
    this.produtoService.deletarFoto(this.produto.id, fotoId).subscribe({
      next: () => {
        if(this.produto){
          this.produto.fotos = this.produto.fotos.filter(x => x.id != fotoId);
        }
      }
    })
  }

  initializeUploader(){
    this.uploader = new FileUploader({
      url: this.baseUrl + 'produto/add-foto/' + this.produto?.id,
      authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if(response) {
        const foto = JSON.parse(response);
        if(!this.produto) return;
        this.produto.fotos.push(foto)
        if(foto.isMain && this.user){
          this.produto.fotos = foto.url;
          this.accountService.setCurrentUser(this.user);
        }
      }
    }
  }
}
