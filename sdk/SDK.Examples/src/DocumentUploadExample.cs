using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class DocumentUploadExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new DocumentUploadExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;

        public DocumentUploadExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public DocumentUploadExample(string apiKey, string apiUrl, string email1) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            // 1. Create a package
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "Policy " + DateTime.Now )
                .DescribedAs( "This is a package created using the e-SignLive SDK" )
                    .ExpiresOn( DateTime.Now.AddMonths(1) )
                    .WithEmailMessage( "This message should be delivered to all signers" )
                    .WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
                                .WithCustomId( "Client1" )
                                .WithFirstName( "John" )
                                .WithLastName( "Smith" )
                                .WithTitle( "Managing Director" )
                                .WithCompany( "Acme Inc." ) )
                    .Build();
            superDuperPackage.Id = eslClient.CreatePackage( superDuperPackage );

            // 2. Construct a document
            Document document = DocumentBuilder.NewDocumentNamed( "First Document" )
                                .FromStream( fileStream1, DocumentType.PDF )
                                .WithSignature( SignatureBuilder.SignatureFor( email1 )
                                .OnPage( 0 )
                                .WithField( FieldBuilder.CheckBox()
                                .OnPage( 0 )
                                .AtPosition( 400, 200 )
                                .WithValue( "x" ) )
                                .AtPosition( 100, 100 ) )
                    .Build();

            // 3. Attach the document to the created package by uploading the document.
            eslClient.UploadDocument(document.FileName, document.Content, document, superDuperPackage);

            eslClient.SendPackage(superDuperPackage.Id);

            SessionToken sessionToken = eslClient.CreateSessionToken( superDuperPackage.Id, "Client1" );

        }
    }
}

