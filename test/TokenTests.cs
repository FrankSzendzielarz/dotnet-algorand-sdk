using NUnit.Framework;
using Algorand;
using System;
using System.Collections.Generic;
using System.Text;
using Algorand.Token;

namespace test
{
    [TestFixture]
    internal class TokenTests
    {
        [Test]
        public void TestArc3JsonSerializationHysterisis1()
        {


            Algorand.Token.TokenMetadata newArcAsset = new Algorand.Token.TokenMetadata()
            {
                AnimationUrl = new Uri(@"https://s3.amazonaws.com/your-bucket/song/preview/mysong.ogg"),
                AnimationUrlIntegrity = "sha256-LwArA6xMdnFF3bvQjwODpeTG/RVn61weQSuoRyynA1I=",
                AnimationUrlMimetype = "audio/ogg",
                BackgroundColor = System.Drawing.Color.Aquamarine,
                Decimals = 3,
                Description = "My first and best song!",
                ExternalUrl = new Uri("https://mysongs.com/song/mysong"),
                ExternalUrlIntegrity = "sha256-LwArA6xMdnFF3bvQjwODpeTG/RVn61weQSuoRyynA1I=",
                ExternalUrlMimetype = "audio/ogg",
                ExtraMetadata = "iHcUslDaL/jEM/oTxqEX++4CS8o3+IZp7/V5Rgchqwc=",
                Image = "https://s3.amazonaws.com/your-bucket/song/cover/mysong.png",
                ImageIntegrity = "https://s3.amazonaws.com/your-bucket/song/cover/mysong.png",
                ImageMimetype = "image/png",
                Name = "My Song",
                Properties = new
                {
                    SimpleProperty = "example_value",
                    RichProperty = new
                    {
                        Name = "Name",
                        Value = "123",
                        DisplayValue = "123 Example Value",
                        @Class = "emphasis",
                        Css = new
                        {
                            Color = "#fffffff",
                            FontWeight = "bold",
                            TextDecoration = "underline"
                        }


                    },

                }


            };

            var a = newArcAsset.ToJson();

            var b = TokenMetadata.FromJson(a);

            var c = b.ToJson();

            Assert.AreEqual(a, c);


        }


        private class SomeObject
        {
            public class NestedObject
            {
                public int NestedId { get; set; }
            }

            public string Name { get; set; }
            public decimal Value { get; set; }

            public NestedObject Something { get; set; }

        }

        [Test]
        public void TestArc3JsonSerializationHysterisis2()
        {


            SomeObject properties = new SomeObject()
            {
                Name = "Belouis Some",
                Something = new SomeObject.NestedObject()
                {
                    NestedId = 1,
                },
                Value = 1.2M
            };

            Algorand.Token.TokenMetadata newArcAsset = new Algorand.Token.TokenMetadata()
            {
                AnimationUrl = new Uri(@"https://s3.amazonaws.com/your-bucket/song/preview/mysong.ogg"),
                AnimationUrlIntegrity = "sha256-LwArA6xMdnFF3bvQjwODpeTG/RVn61weQSuoRyynA1I=",
                AnimationUrlMimetype = "audio/ogg",
                BackgroundColor = System.Drawing.Color.Aquamarine,
                Decimals = 3,
                Description = "My first and best song!",
                ExternalUrl = new Uri("https://mysongs.com/song/mysong"),
                ExternalUrlIntegrity = "sha256-LwArA6xMdnFF3bvQjwODpeTG/RVn61weQSuoRyynA1I=",
                ExternalUrlMimetype = "audio/ogg",
                ExtraMetadata = "iHcUslDaL/jEM/oTxqEX++4CS8o3+IZp7/V5Rgchqwc=",
                Image = "https://s3.amazonaws.com/your-bucket/song/cover/mysong.png",
                ImageIntegrity = "https://s3.amazonaws.com/your-bucket/song/cover/mysong.png",
                ImageMimetype = "image/png",
                Name = "My Song",
                Properties = properties


            };

            var a = newArcAsset.ToJson();

            var b = TokenMetadata.FromJson(a);

            var c = b.ToJson();

            Assert.AreEqual(a, c);


        }
        private static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        [Test]
        public void TestArc3GenerateAsset()
        {


            SomeObject properties = new SomeObject()
            {
                Name = "Belouis Some",
                Something = new SomeObject.NestedObject()
                {
                    NestedId = 1,
                },
                Value = 1.2M
            };

            Algorand.Token.TokenMetadata newArcAsset = new Algorand.Token.TokenMetadata()
            {
                AnimationUrl = new Uri(@"https://s3.amazonaws.com/your-bucket/song/preview/mysong.ogg"),
                AnimationUrlIntegrity = "sha256-LwArA6xMdnFF3bvQjwODpeTG/RVn61weQSuoRyynA1I=",
                AnimationUrlMimetype = "audio/ogg",
                BackgroundColor = System.Drawing.Color.Aquamarine,
                Decimals = 3,
                Description = "My first and best song!",
                ExternalUrl = new Uri("https://mysongs.com/song/mysong"),
                ExternalUrlIntegrity = "sha256-LwArA6xMdnFF3bvQjwODpeTG/RVn61weQSuoRyynA1I=",
                ExternalUrlMimetype = "audio/ogg",
                ExtraMetadata = "iHcUslDaL/jEM/oTxqEX++4CS8o3+IZp7/V5Rgchqwc=",
                Image = "https://s3.amazonaws.com/your-bucket/song/cover/mysong.png",
                ImageIntegrity = "https://s3.amazonaws.com/your-bucket/song/cover/mysong.png",
                ImageMimetype = "image/png",
                Name = "My Song",
                Properties = properties


            };


            string account1_mnemonic = "portion never forward pill lunch organ biology weird catch curve isolate plug innocent skin grunt bounce clown mercy hole eagle soul chunk type absorb trim";
            Account acct1 = new Account(account1_mnemonic);


            var ap = Algorand.Token.Utils.GeneratePureNonFungibleTokenParameters(newArcAsset, new Uri(@"https://www.google.com"), acct1, "DOL", "EUR");

            Assert.AreEqual(ByteArrayToString(ap.MetadataHash), "4D73763BB44135943AE0F385F24ED120DCDB0DB695528406A8AD96550E594DE0");
            Assert.AreEqual(ap.Url, new Uri(@"https://www.google.com").ToString());
     

        }
    }
}
