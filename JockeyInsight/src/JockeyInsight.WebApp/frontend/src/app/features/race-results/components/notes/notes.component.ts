import {Component, EventEmitter, Input, Output, ViewChild, ElementRef} from '@angular/core';
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'notes',
  templateUrl: './notes.component.html',
  standalone: true,
  imports: [
    FormsModule
  ], // Add FormsModule or CommonModule if needed
})
export class NoteModalComponent {
  @Input() raceStatId: string = '';
  @Input() initialNote: string = '';
  @Input() id: number | undefined = 0;
  @Output() noteSaved = new EventEmitter<{ raceStatId: string; body: string; id:any }>();

  noteBody: string = '';

  @ViewChild('openModalButton') openModalButton!: ElementRef<HTMLButtonElement>;
  @ViewChild('closeModalButton') closeModalButton!: ElementRef<HTMLButtonElement>;

  open(): void {
    this.noteBody = this.initialNote || '';
    this.openModalButton.nativeElement.click();
  }

  save(): void {
    this.noteSaved.emit({
      raceStatId: this.raceStatId,
      body: this.noteBody,
      id: this.id,
    });

    this.closeModalButton.nativeElement.click();
  }
}
