export interface Message {
    id: number;
    senderId: string;
    senderKnownAs: string;
    senderPhotoUrl: string;
    recipientId: string;
    recipientKnownAs: string;
    recipientPhotoUrl: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    messageSent: Date;
}
