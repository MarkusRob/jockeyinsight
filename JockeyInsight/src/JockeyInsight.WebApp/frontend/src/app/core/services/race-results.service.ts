import {HttpClient, HttpParams} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class RaceResultsService {
  private readonly baseUrl = '/api/v1/raceresults';

  constructor(private http: HttpClient) {
  }

  getRaceResults(pageNumber: number, pageSize: number, horseName?: string, raceName?: string, raceCourse?: string): Observable<any> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize)
      .set('horseName', horseName || '')
      .set('raceName', raceName || '')
      .set('raceCourse', raceCourse || '');

    return this.http.get(`${this.baseUrl}`, { params });
  }
}
