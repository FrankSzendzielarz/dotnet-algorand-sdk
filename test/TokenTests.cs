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
        public void TestArc3JsonSerialization()
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
    }
}
