namespace Infrastructure.Dto
{
    public static class ContratoTextoBaseDto
    {
        public static string ObtenerTexto()
        {
            return @"
Yo, **{NOMBRE}**, con domicilio en __{DIRECCION}__, declaro bajo juramento que las declaraciones contenidas en el presente documento reflejan fielmente mi voluntad y compromiso legal. Reconozco que toda la información aquí expresada es verídica y será utilizada exclusivamente para fines contractuales. Este documento tiene carácter vinculante a partir de su firma, y ha sido elaborado de manera libre, consciente y sin coacción de ninguna de las partes involucradas. Ambas partes han contado con el tiempo y los recursos necesarios para revisar, comprender y aceptar cada una de las cláusulas aquí estipuladas. //La firma de este contrato implica la aceptación total y sin reservas de todas sus disposiciones.//

El presente acuerdo establece los términos y condiciones mediante los cuales se desarrollará la relación jurídica entre los firmantes. **Cláusulas específicas** como plazos de cumplimiento, responsabilidades mutuas, medidas de seguridad, y causas de terminación anticipada han sido definidas con claridad en las secciones correspondientes. El documento también contempla disposiciones sobre **confidencialidad**, __resolución de conflictos__, e indemnización en caso de incumplimiento. Se estipula que, en caso de desacuerdo, las partes deberán recurrir a procesos de conciliación antes de acudir a instancias judiciales, salvo en casos de urgencia debidamente justificados. Cualquier modificación al presente contrato deberá realizarse por escrito y con el consentimiento de ambas partes.

__Firmado en Tegucigalpa, Honduras, a los seis días del mes de agosto del año dos mil veinticinco.__ El firmante, **{NOMBRE}**, manifiesta su conformidad con cada uno de los puntos anteriores. Asimismo, se compromete a respetar y cumplir lo establecido en este acuerdo, actuando en todo momento con integridad y buena fe. Este documento podrá ser presentado ante autoridades competentes como prueba del vínculo legal existente entre las partes. __Para constancia, se firma en dos ejemplares idénticos, quedando uno en poder de cada parte.__
";

        }
    }
}
