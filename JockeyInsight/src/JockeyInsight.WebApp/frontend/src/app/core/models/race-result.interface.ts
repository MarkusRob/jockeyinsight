import {Note} from "./notes.interface";

export interface RaceResult {
  raceCourse: string;
  id: string;
  race: string;
  raceDate: string;
  raceTime: string;
  raceDistance: string;
  horse: string;
  jockey: string;
  trainer: string;
  finishingPosition: number;
  distanceBeaten: string;
  timeBeaten: string;
  note?: Note;
}
