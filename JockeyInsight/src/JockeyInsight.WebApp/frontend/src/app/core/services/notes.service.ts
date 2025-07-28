import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  private readonly baseUrl = '/api/v1/notes';

  constructor(private http: HttpClient) {}
  addNote(note: any): Observable<void> {
    return this.http.post<void>(this.baseUrl, note);
  }
}
