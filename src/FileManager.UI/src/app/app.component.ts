import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { FileDownloadResponse, FileModel, UserInfo } from '../models/file.model';
import { AsyncPipe, CommonModule } from '@angular/common';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HttpClientModule, AsyncPipe, CommonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FileManager.UI';
  http = inject(HttpClient);

  login$ = this.login();
  userFiles$ = this.getUserFiles();
  selectedFile: File | null = null;

  private login(): Observable<UserInfo> {
    return this.http.get<UserInfo>(`${environment.apiUrl}/Account/login`, {
      withCredentials: true,
    });
  }

  private getUserFiles(): Observable<FileModel[]> {
    return this.http.get<FileModel[]>(`${environment.apiUrl}/Files`, {
      withCredentials: true,
    });
  }

  uploadFile(file: File): void {
    if (!file) return;

    const formData = new FormData();
    formData.append('File', file);

    this.http.post<FileModel>(`${environment.apiUrl}/Files/upload`, formData, {
      withCredentials: true,
    }).subscribe({
      next: () => {
        this.userFiles$ = this.getUserFiles();
      },
      error: (errorResponse) => {
        if (errorResponse.status === 409 && errorResponse.error) {
          alert(`Ошибка: ${errorResponse.error}`);
        } else {
          alert('Неизвестная ошибка');
        }
      },
    });
  }

  downloadFile(fileId: number): void {
    this.http.get<FileDownloadResponse>(`${environment.apiUrl}/Files/download/${fileId}`, {
      withCredentials: true,
    }).subscribe(response => {
        let blob: Blob;

        if (typeof response.fileContent === 'string') {
          const byteCharacters = atob(response.fileContent);
          const byteNumbers = new Array(byteCharacters.length);
          for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
          }
          blob = new Blob([new Uint8Array(byteNumbers)]);
        }

        else if (Array.isArray(response.fileContent)) {
          blob = new Blob([new Uint8Array(response.fileContent)]);
        }

        else {
          blob = new Blob([response.fileContent]);
        }

        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = response.fileName;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        window.URL.revokeObjectURL(url);
      });
  }

  deleteFile(fileId: number): void {
    this.http.delete<void>(`${environment.apiUrl}/Files/${fileId}`, {
      withCredentials: true,
    }).subscribe({
      next: () => {
        this.userFiles$ = this.getUserFiles();
      },
      error: () => alert('Ошибка при удалении файла'),
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input?.files?.length) {
      this.selectedFile = input.files[0];
    }
  }
}
