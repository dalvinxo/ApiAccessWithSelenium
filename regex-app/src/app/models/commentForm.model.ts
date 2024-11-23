export interface commentForm {
  id?: number;
  content: string;
  createdDate: string;
  parentCommentId?: number | null;
}
