import {Component, OnInit, ViewChild} from '@angular/core';
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {RaceResultsService} from "../../core/services/race-results.service";
import {RaceResult} from "../../core/models/race-result.interface";
import {NoteModalComponent} from "./components/notes/notes.component";
import {NotesService} from "../../core/services/notes.service";
import {NgbPagination} from "@ng-bootstrap/ng-bootstrap";
import {FormsModule} from "@angular/forms";
import {debounceTime, Subject} from "rxjs";


@Component({
  selector: 'app-race-results',
  templateUrl: './race-results.component.html',
  imports: [
    NgForOf,
    NgIf,
    NoteModalComponent,
    DatePipe,
    NgbPagination,
    FormsModule
  ],
  standalone: true
})
export class RaceResultsComponent implements OnInit {
  raceResults: RaceResult[] = [];
  @ViewChild(NoteModalComponent) noteModal!: NoteModalComponent;
  selectedResult!: RaceResult;
  totalCount = 0;
  pageNumber = 1;
  pageSize = 10;
  horseName = '';
  raceName = '';
  raceCourse = '';
  searchChanged = new Subject<void>;
  constructor(private raceResultsService: RaceResultsService,private notesService: NotesService) {}

  ngOnInit(): void {
    this.searchChanged
      .pipe(debounceTime(500))
      .subscribe(() => {
        this.pageNumber = 1;
        this.loadRaceResults();
      });

    this.loadRaceResults();
  }
  loadRaceResults(): void {
    this.raceResultsService
      .getRaceResults(
        this.pageNumber,
        this.pageSize,
        this.horseName.trim(),
        this.raceName.trim(),
        this.raceCourse.trim()
      )
      .subscribe({
        next: (data) => {
          this.raceResults = data.items;
          this.totalCount = data.totalCount;
        },
        error: (err) => console.error('Failed to load race results', err)
      });
  }
  onSearchChanged() {
    this.pageNumber = 1;
    this.searchChanged.next();
  }
  onPageChange(page: number) {
    this.pageNumber = page;
    this.loadRaceResults();
  }
  onAddEditNote(result: RaceResult): void {
    this.selectedResult = result;
    this.noteModal.raceStatId = result.id;
    this.noteModal.initialNote = result.note?.body || '';
    this.noteModal.id = result.note?.id
    this.noteModal.open();
  }

  onNoteSaved(note: { raceStatId: string; body: string; }) {
    this.notesService.addNote(note).subscribe({
      next: () => {
       window.location.reload();
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}
