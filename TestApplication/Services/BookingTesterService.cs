using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace TestApplication.Services
{
    public class BookingTesterService
    {

        readonly IPage page;

        public BookingTesterService()
        {

            var playwright = Playwright.CreateAsync().GetAwaiter().GetResult();

            var browser = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,

            }).GetAwaiter().GetResult();
            var context = browser.NewContextAsync(//new BrowserNewContextOptions
            /*{
                RecordVideoDir = "videos/"
            }*/).GetAwaiter().GetResult();

            // Open new page
            page = context.NewPageAsync().GetAwaiter().GetResult();


        }



        void SelectHotelAndCeckin(string weburl, DateTime? chin = null, DateTime? chout = null, int? AdultsNumber = 1, int? ChildrenNumber = 1, List<int> childAge = null)
        {
            //otel seç
            //chin - chout seç
            //yetişkin çocuk seç


            // Go to weburl
            //page.GotoAsync(weburl).GetAwaiter().GetResult();

            // Go to ${weburl}/tr/book-rooms
            page.GotoAsync($"{weburl}/tr/book-rooms").GetAwaiter().GetResult();

            // Click text= Hotel
            page.DispatchEventAsync("[type=radio][id*=Hotel]", "click").GetAwaiter().GetResult();

            DateTime current_date = DateTime.Now;
            DateTime TenDaysLater = current_date.AddDays(10);
            DateTime TwentyDaysLater = TenDaysLater.AddDays(10);
            string date1 = TenDaysLater.ToString("dd.MM.yyyy hh:mm:ss");
            string date2 = TwentyDaysLater.ToString("dd.MM.yyyy hh:mm:ss");

            if (chin.HasValue)
            {
                date1 = chin.Value.ToString("dd.MM.yyyy");
            }

            if (chout.HasValue)
            {
                date2 = chout.Value.ToString("dd.MM.yyyy");
            }

            page.EvaluateHandleAsync($"var element = document.querySelector('input#checkindate, input#inpCheckinDate'); element.value='{date1}';").GetAwaiter().GetResult();
            page.EvaluateHandleAsync($"var element = document.querySelector('input#checkoutdate, input#inpCheckoutDate'); element.value='{date2}';").GetAwaiter().GetResult();


            page.EvaluateHandleAsync($"var element = document.getElementById('inpAdultCount'); element.value = {AdultsNumber}; " +
                       $"var element2 = document.getElementById('inpChildCount'); element2.value = {ChildrenNumber}; ").GetAwaiter().GetResult();

            for (int i = 0; i < ChildrenNumber; i++)
            {

                page.EvaluateHandleAsync($"var element = document.querySelector('[name=childAge{i + 1}]'); element.value = {childAge[i]}; ").GetAwaiter().GetResult();

            }

            // Click text=Oda Ara

            page.WaitForTimeoutAsync(1000).GetAwaiter().GetResult();
            page.RunAndWaitForNavigationAsync(() =>
            {
                page.DispatchEventAsync("#checkAvailablity, .book-now", "click").GetAwaiter().GetResult();
                return Task.CompletedTask;
            }).GetAwaiter().GetResult();


            // Click #bookForm >> text=SEÇ
            page.RunAndWaitForNavigationAsync(() =>
            {
                page.DispatchEventAsync("[data-book='book']", "click").GetAwaiter().GetResult();
                return Task.CompletedTask;
            }).GetAwaiter().GetResult();

        }

        void SelectTransfers()
        {
            //transfer istiyorum
            //transfer türü seç
            //transfer bilgileri gir
            //misafir bilgilerine devam et


            // Click text=Transfer istiyorum
            page.DispatchEventAsync("input#NeedTransfer", "click").GetAwaiter().GetResult();

            page.EvaluateHandleAsync("var element = document.getElementById('DirectionOnlyArrival'); " +
                "if(document.body.contains(element)==true){element.click()};").GetAwaiter().GetResult();

            // Click text=Sadece Geliş
            //page.DispatchEventAsync("[for='DirectionOnlyArrival']", "click").GetAwaiter().GetResult();          condition eklemek için js kullandım ^^^^

            // Click text=SEÇ 850 TRY
            //page.DispatchEventAsync("input#ArrivalTransfer_9", "click").GetAwaiter().GetResult();
            page.DispatchEventAsync("[name='Arrival.Flight.Airport.Id']", "click").GetAwaiter().GetResult();

            // Click [placeholder="Uçuş Numarası Giriniz"]
            page.DispatchEventAsync("[name='Arrival.Flight.Number']", "click").GetAwaiter().GetResult();

            // Fill [placeholder="Uçuş Numarası Giriniz"]
            page.FillAsync("[name='Arrival.Flight.Number']", "UçuşNumarasıTest").GetAwaiter().GetResult();

            page.DispatchEventAsync("[name='Arrival.Flight.Time']", "click").GetAwaiter().GetResult();

            // Fill [placeholder="[HH:mm] Uçuş Saati"]
            page.FillAsync("[name='Arrival.Flight.Time']", "12:00").GetAwaiter().GetResult();

            // Click [placeholder="Notunuz"]
            page.DispatchEventAsync("[name='Arrival.Note']", "click").GetAwaiter().GetResult();

            // Fill [placeholder="Notunuz"]
            page.FillAsync("[name='Arrival.Note']", "KullanıcıNotuTest").GetAwaiter().GetResult();

            // Click #btnContinue >> text=DEVAM ET
            //page.DispatchEventAsync("a.btn.btn--continue", "click").GetAwaiter().GetResult();
            // Assert.Equal($"{weburl}/tr/book-guests", page.Url);

            // Click text=Devam Et
            //page.ClickAsync("text=Devam Et").GetAwaiter().GetResult();                                                        
            page.RunAndWaitForNavigationAsync(async () =>
            {
                await page.DispatchEventAsync("#btnContinue", "click");

            }).GetAwaiter().GetResult();
        }

        void FillGuestInfos(int? AdultsNumber = 1, int? ChildrenNumber = 1, List<string> adultNames = null, List<string> adultSurnames = null, List<string> childNames = null, List<string> childSurnames = null)
        {
            //Konaklayan kişilerin bilgilerini gir.
            //zorunlu alanları doldur
            //ödemeye devam et

            // Click text=Detaylı Bilgi
            page.DispatchEventAsync("input#inpCompleteInfo,input#inpFillFieldOfAllGuests", "click").GetAwaiter().GetResult();

            for (int i = 0; i < AdultsNumber; i++)
            {


                // Select gender
                page.SelectOptionAsync($"select[name=\"GuestsModel.Adults[{i}].Gender\"]", new[] { "2" }).GetAwaiter().GetResult();

                // Click text=2. Misafir Bilgileri ErkekKadın >> [placeholder="Ad"]
                page.DispatchEventAsync($"[name='GuestsModel.Adults[{i}].Name.FirstName']", "click").GetAwaiter().GetResult();

                // Fill text=2. Misafir Bilgileri ErkekKadın >> [placeholder="Ad"]
                page.FillAsync($"[name='GuestsModel.Adults[{i}].Name.FirstName']", adultNames[i]).GetAwaiter().GetResult();

                // Click text=2. Misafir Bilgileri ErkekKadın >> [placeholder="Soyad"]
                page.DispatchEventAsync($"[name='GuestsModel.Adults[{i}].Name.LastName']", "click").GetAwaiter().GetResult();

                // Fill text=2. Misafir Bilgileri ErkekKadın >> [placeholder="Soyad"]
                page.FillAsync($"[name='GuestsModel.Adults[{i}].Name.LastName']", adultSurnames[i]).GetAwaiter().GetResult();



            }


            for (int i = 0; i < ChildrenNumber; i++)
            {

                // Click text=2. Misafir Bilgileri ErkekKadın >> [placeholder="Ad"]
                page.DispatchEventAsync($"[name=\"GuestsModel.Childern[{i}].Name.FirstName\"]", "click").GetAwaiter().GetResult();

                // Fill text=2. Misafir Bilgileri ErkekKadın >> [placeholder="Ad"]
                page.FillAsync($"[name=\"GuestsModel.Childern[{i}].Name.FirstName\"]", childNames[i]).GetAwaiter().GetResult();

                // Click text=2. Misafir Bilgileri ErkekKadın >> [placeholder="Soyad"]
                page.DispatchEventAsync($"[name=\"GuestsModel.Childern[{i}].Name.LastName\"]", "click").GetAwaiter().GetResult();

                // Fill text=2. Misafir Bilgileri ErkekKadın >> [placeholder="Soyad"]
                page.FillAsync($"[name=\"GuestsModel.Childern[{i}].Name.LastName\"]", childSurnames[i]).GetAwaiter().GetResult();



            }




            // Click [placeholder="E-Posta*"]
            page.DispatchEventAsync("#email", "click").GetAwaiter().GetResult();

            // Fill [placeholder="E-Posta*"]
            page.FillAsync("#email", "gunduzoglu98@gmail.com").GetAwaiter().GetResult();

            // Click [placeholder="TC Kimlik No*"]
            page.DispatchEventAsync("[name='Billing.IdentityNo']", "click").GetAwaiter().GetResult();

            // Fill [placeholder="TC Kimlik No*"]
            page.FillAsync("[name='Billing.IdentityNo']", "15860798318").GetAwaiter().GetResult();

            // Click [placeholder="Telefon*"]
            page.DispatchEventAsync("[name='Billing.Contact.Phone']", "click").GetAwaiter().GetResult();

            // Fill [placeholder="Telefon*"]
            page.FillAsync("[name='Billing.Contact.Phone']", "5546811363").GetAwaiter().GetResult();

            // Click textarea[name="Billing.Contact.Address"]
            page.DispatchEventAsync("#Billing_Contact_Address", "click").GetAwaiter().GetResult();

            // Fill textarea[name="Billing.Contact.Address"]
            page.FillAsync("#Billing_Contact_Address", "AdresTest").GetAwaiter().GetResult();


            // Click text=Kişisel Verilerin Korunması Kanunu ve Gizlilik Poliçesini okudum*
            page.EvaluateHandleAsync(
                "var element = document.getElementById('inpTerms');element.checked=true;" +
                "element.classList.remove('is-invalid'); " +
                "element.classList.add('is-valid'); " +
                "element.arialCurrent='true'").GetAwaiter().GetResult();


            // Click #btnContinue >> text=ÖDEME YAP
            page.RunAndWaitForNavigationAsync(async () =>
            {
                await page.DispatchEventAsync("#btnContinue", "click");

            }).GetAwaiter().GetResult();
        }

        void CompletePaymentWithCreditCard()
        {

            // Click [placeholder="Kredi Kartı Üzerinde Yazan İsim"]
            page.DispatchEventAsync("#cardHolderName", "click").GetAwaiter().GetResult();

            // Fill [placeholder="Kredi Kartı Üzerinde Yazan İsim"]
            page.FillAsync("#cardHolderName", "Egemen Gündüz").GetAwaiter().GetResult();

            // Click [placeholder="Kredi Kartı Numarası"]
            page.DispatchEventAsync("#cardNumber", "click").GetAwaiter().GetResult();

            // Fill [placeholder="Kredi Kartı Numarası"]
            page.FillAsync("#cardNumber", "4824650150457775").GetAwaiter().GetResult();

            // Click [placeholder="Cvv Kodu"]
            page.DispatchEventAsync("#cvvNumber", "click").GetAwaiter().GetResult();

            // Fill [placeholder="Cvv Kodu"]
            page.FillAsync("#cvvNumber", "570").GetAwaiter().GetResult();

            // Click text=Online Satış Sözleşmesini okudum ve anladım.
            page.EvaluateHandleAsync("var element = document.getElementById('inpOnlineSalesAgreement'); " +
                "element.classList.add('is-active');" +
                " element.checked=true;").GetAwaiter().GetResult();


            //page.ScreenshotAsync().GetAwaiter().GetResult();

            // Click #btnContinue >> text=ÖDEMEYİ TAMAMLA
            /*page.RunAndWaitForNavigationAsync(() =>
            {
                page.DispatchEventAsync("a.btn.btn--continue", "click").GetAwaiter().GetResult();
                return Task.CompletedTask;
            }).GetAwaiter().GetResult();*/
        }


        void CompletePaymentWithTransfer()
        {


            // Click text=Havale
            page.EvaluateHandleAsync("document.querySelector('[for=\"inpMoneyOrder\"]').click();").GetAwaiter().GetResult();


            // Click label:has-text("Vakifbank")
            page.DispatchEventAsync("[name='BankAccountId']", "click").GetAwaiter().GetResult();

            // Click text=Online Satış Sözleşmesini okudum ve anladım.
            page.EvaluateHandleAsync("var element = document.getElementById('inpOnlineSalesAgreement'); " +
                "element.classList.add('is-active');" +
                "element.checked=true;").GetAwaiter().GetResult();

            // Click textarea[name="Note"]
            page.DispatchEventAsync("#txtReservationNote", "click").GetAwaiter().GetResult();

            // Fill textarea[name="Note"]
            page.FillAsync("#txtReservationNote", "RezervasyonNotuTest").GetAwaiter().GetResult();


            /*// Click #btnContinue >> text=ÖDEMEYİ TAMAMLA
            page.RunAndWaitForNavigationAsync(() => {
                page.DispatchEventAsync("a.btn.btn--continue", "click").GetAwaiter().GetResult();
                return Task.CompletedTask;
            }).GetAwaiter().GetResult();

            // Click text=Ana Sayfaya Dön
            page.RunAndWaitForNavigationAsync(() => {
                page.DispatchEventAsync("a.btn.asteria__booking-success-button", "click").GetAwaiter().GetResult();
                return Task.CompletedTask;
            }).GetAwaiter().GetResult();*/



        }


        public void TestBooking(string weburl, int? AdultsNumber = 1, int? ChildrenNumber = 1, List<int> ChildAge = null,
            List<string> AdultNames = null, List<string> AdultSurnames = null, List<string> ChildNames = null, List<string> ChildSurnames = null)
        {
            SelectHotelAndCeckin(weburl, DateTime.Now.AddDays(20), DateTime.Now.AddDays(25), AdultsNumber, ChildrenNumber, ChildAge);
            SelectTransfers();
            FillGuestInfos(AdultsNumber, ChildrenNumber, AdultNames, AdultSurnames, ChildNames, ChildSurnames);
            //CompletePaymentWithCreditCard();
            CompletePaymentWithTransfer();
        }

    }
}
