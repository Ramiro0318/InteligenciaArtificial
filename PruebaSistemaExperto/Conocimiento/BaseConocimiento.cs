using chen0040.ExpertSystem;

namespace SistemaExpertoPlantas
{
    public static class BaseConocimiento
    {
        public static void CargarReglas(RuleInferenceEngine motor)
        {
            Rule regla;

            // ==================================================
            // CACTUS
            // ==================================================

            regla = new Rule("R1_AdaptacionSeca");
            regla.AddAntecedent(new IsClause("tieneEspinas", "si"));
            regla.AddAntecedent(new IsClause("ambiente", "seco"));
            regla.setConsequent(new IsClause("adaptacionSeca", "si"));
            motor.AddRule(regla);

            regla = new Rule("R2_CandidatoCactus");
            regla.AddAntecedent(new IsClause("adaptacionSeca", "si"));
            regla.AddAntecedent(new IsClause("tieneEspinas", "si"));
            regla.setConsequent(new IsClause("candidatoCactus", "si"));
            motor.AddRule(regla);

            regla = new Rule("R3_ClasificarCactus");
            regla.AddAntecedent(new IsClause("candidatoCactus", "si"));
            regla.setConsequent(new IsClause("clasificacionFinal", "cactus"));
            motor.AddRule(regla);

            // ==================================================
            // HELECHO
            // ==================================================

            regla = new Rule("R4_CaracteristicasHelecho");
            regla.AddAntecedent(new IsClause("reproduccionEsporas", "si"));
            regla.AddAntecedent(new IsClause("tipoHoja", "frondosa"));
            regla.setConsequent(new IsClause("caracteristicasHelecho", "si"));
            motor.AddRule(regla);

            regla = new Rule("R5_CandidatoHelecho");
            regla.AddAntecedent(new IsClause("caracteristicasHelecho", "si"));
            regla.AddAntecedent(new IsClause("tieneFlores", "no"));
            regla.setConsequent(new IsClause("candidatoHelecho", "si"));
            motor.AddRule(regla);

            regla = new Rule("R6_ClasificarHelecho");
            regla.AddAntecedent(new IsClause("candidatoHelecho", "si"));
            regla.setConsequent(new IsClause("clasificacionFinal", "helecho"));
            motor.AddRule(regla);

            // ==================================================
            // ALTURA
            // ==================================================

            regla = new Rule("R7_AlturaBaja");
            regla.AddAntecedent(new IsClause("altura", "baja"));
            regla.setConsequent(new IsClause("alturaBaja", "si"));
            motor.AddRule(regla);

            regla = new Rule("R8_AlturaMedia");
            regla.AddAntecedent(new IsClause("altura", "media"));
            regla.setConsequent(new IsClause("alturaMedia", "si"));
            motor.AddRule(regla);

            regla = new Rule("R9_AlturaAlta");
            regla.AddAntecedent(new IsClause("altura", "alta"));
            regla.setConsequent(new IsClause("alturaAlta", "si"));
            motor.AddRule(regla);

            // ==================================================
            // PLANTA HERBACEA
            // ==================================================

            regla = new Rule("R10_CandidatoHerbacea");
            regla.AddAntecedent(new IsClause("tipoTallo", "herbaceo"));
            regla.AddAntecedent(new IsClause("alturaBaja", "si"));
            regla.AddAntecedent(new IsClause("tieneEspinas", "no"));
            regla.AddAntecedent(new IsClause("reproduccionEsporas", "no"));
            regla.setConsequent(new IsClause("candidatoHerbacea", "si"));
            motor.AddRule(regla);

            regla = new Rule("R11_RaizHerbacea");
            regla.AddAntecedent(new IsClause("tipoRaiz", "fibrosa"));
            regla.AddAntecedent(new IsClause("tipoTallo", "herbaceo"));
            regla.AddAntecedent(new IsClause("tieneEspinas", "no"));
            regla.AddAntecedent(new IsClause("reproduccionEsporas", "no"));
            regla.setConsequent(new IsClause("candidatoHerbacea", "si"));
            motor.AddRule(regla);

            regla = new Rule("R12_ClasificarHerbacea");
            regla.AddAntecedent(new IsClause("candidatoHerbacea", "si"));
            regla.setConsequent(
                new IsClause("clasificacionFinal", "planta_herbacea")
            );
            motor.AddRule(regla);

            // ==================================================
            // ARBUSTO
            // ==================================================

            regla = new Rule("R13_CandidatoArbusto");
            regla.AddAntecedent(new IsClause("tipoTallo", "lenoso"));
            regla.AddAntecedent(new IsClause("alturaMedia", "si"));
            regla.AddAntecedent(new IsClause("tieneEspinas", "no"));
            regla.setConsequent(new IsClause("candidatoArbusto", "si"));
            motor.AddRule(regla);

            regla = new Rule("R14_ClasificarArbusto");
            regla.AddAntecedent(new IsClause("candidatoArbusto", "si"));
            regla.setConsequent(new IsClause("clasificacionFinal", "arbusto"));
            motor.AddRule(regla);

            // ==================================================
            // ARBOL
            // ==================================================

            regla = new Rule("R15_CandidatoArbol");
            regla.AddAntecedent(new IsClause("tipoTallo", "lenoso"));
            regla.AddAntecedent(new IsClause("alturaAlta", "si"));
            regla.AddAntecedent(new IsClause("tieneEspinas", "no"));
            regla.setConsequent(new IsClause("candidatoArbol", "si"));
            motor.AddRule(regla);

            regla = new Rule("R16_ClasificarArbol");
            regla.AddAntecedent(new IsClause("candidatoArbol", "si"));
            regla.setConsequent(new IsClause("clasificacionFinal", "arbol"));
            motor.AddRule(regla);

            // ==================================================
            // FLORES Y FRUTOS
            // ==================================================

            regla = new Rule("R17_PlantaConFlores");
            regla.AddAntecedent(new IsClause("tieneFlores", "si"));
            regla.setConsequent(new IsClause("plantaConFlores", "si"));
            motor.AddRule(regla);

            regla = new Rule("R18_PlantaConFrutos");
            regla.AddAntecedent(new IsClause("plantaConFlores", "si"));
            regla.AddAntecedent(new IsClause("tieneFrutos", "si"));
            regla.setConsequent(new IsClause("plantaConFrutos", "si"));
            motor.AddRule(regla);
        }
    }
}