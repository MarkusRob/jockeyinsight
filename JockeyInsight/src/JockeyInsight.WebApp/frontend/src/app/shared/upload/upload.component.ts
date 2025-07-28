import {Component, ElementRef, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {UploadService} from "../../core/services/upload.service";
import {NgIf} from "@angular/common";

@Component({
  selector: 'upload',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './upload.component.html'
})
export class UploadComponent {
  selectedFile: File | null = null;
  isUploading = false;
  uploadError: string | null = null;
  @ViewChild('closeModalButton') closeModalButton!: ElementRef<HTMLButtonElement>;
  constructor(private uploadService: UploadService, private router: Router) {}

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input?.files?.length) {
      this.selectedFile = input.files[0];
      this.uploadError = null;
    }
  }

  uploadFile(): void {
    if (!this.selectedFile) return;

    this.isUploading = true;

    this.uploadService.uploadFile(this.selectedFile).subscribe({
      next: async () => {
        this.isUploading = false;
        this.selectedFile = null;
        this.uploadError = '';
        this.closeModalButton.nativeElement.click();
        await this.router.navigate(['/race-results']);
      },
      error: (err) => {
        this.isUploading = false;
        this.uploadError = 'Upload failed. Please try again.';
        console.error(err);
      }
    });
  }

}
