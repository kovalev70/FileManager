export interface FileModel {
  id: number;
  fileName: string;
  filePath: string;
  hash: string;
  uploadedAt: Date;
}

export interface UserInfo {
  userName: string;
}

export interface FileDownloadResponse {
  fileName: string;
  fileContent: number[];
}
