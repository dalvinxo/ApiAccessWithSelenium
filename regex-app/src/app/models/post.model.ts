import { CommentGame } from './commet.model';

export interface Post {
  comments: CommentGame[];
  description: string;
  gameTitle: string;
  id: number;
  personName: string;
  rate: number;
  thumbnail: string;
  createdDate: string;
  title: string;
}
