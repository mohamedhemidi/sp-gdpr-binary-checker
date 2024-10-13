export interface IEntry {
  Id: string;
  String: string;
  Good: boolean;
  CheckedDate: string;
}

export interface IEntryRequest {
  BinaryString: string;
}
export interface IEntryDeleteRequest {
  Id: string;
}
