

export interface IApartment {
  id: number,
  userName: string,
  postDate: string,
  updateDate: string,
  isActive: boolean,
  isBooking: boolean,
  typeOfBooking: string,
  address: string,
  rooms: number,
  floor: number,
  area: number,
  price: number,
  image: string | null
}

export interface IStreet{
  id: number,
  name: string
}

export interface IAddApartment {
  numberOfBuilding: number| null,
  isPrivateHouse: boolean,
  numberOfRooms: number| null,
  area: number| null,
  price: number| null,
  floor: number| null,
  description: string,
  typeOfBooking: string,
  streetId: number | null,
  streetName: string | null,
  images: File[]
}

export interface IEditApartment {
  id: number | null,
  numberOfBuilding: number | null,
  numberOfRooms: number | null,
  isPrivateHouse: boolean | null,
  isBooking: boolean | null,
  isActive: boolean | null,
  isArchived: boolean| null,
  typeOfBooking: string,
  description: string,
  streetName: string | null,
  existStreetName: string,
  streetId: number | null,
  userName: string,
  userId: number| null,
  rooms: number| null
  floor: number| null,
  area: number| null,
  price: number| null,
  images: File[] | null,
  existImages: string[] | null,
  imagesForDelete: string[] | null

}