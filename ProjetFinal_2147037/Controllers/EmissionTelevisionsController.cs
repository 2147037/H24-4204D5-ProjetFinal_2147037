using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ProjetFinal_2147037.Data;
using ProjetFinal_2147037.Models;

namespace ProjetFinal_2147037.Controllers
{
    public class EmissionTelevisionsController : Controller
    {
        private readonly ProjetFinal_2147037Context _context;

        public EmissionTelevisionsController(ProjetFinal_2147037Context context)
        {
            _context = context;
        }

        // GET: EmissionTelevisions
        public async Task<IActionResult> Index()
        {
            var projetFinal_2147037Context = _context.EmissionTelevisions.Include(e => e.Plateforme);
            return View(await projetFinal_2147037Context.ToListAsync());
        }

        public async Task<IActionResult> IndexAvecViewSQL()
        {
            
            
            return View(await _context.VwActeurPersonnageEmissions.ToListAsync());
        }

        public async Task<IActionResult> InfoUtilisateur()
        {
           

            return View(await _context.Utilisateurs.ToListAsync());
        }

        //Pour voir la liste des utilisateurs et les émissions qu'ils peuvent voir.
        public async Task<IActionResult> DetailsAvancé(int? id)
        { 
            if(id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }
            Utilisateur user = await _context.Utilisateurs.FindAsync(id);
            ViewData["NomUtil"] = user.Pseudo;
            string query = "EXEC Television.uspListeEmission @UtilisateurID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter{ParameterName = "@UtilisateurID", Value = id}
            };
            List<EmissionTelevision> emissionTelevisions = await _context.EmissionTelevisions.FromSqlRaw(query, parameters.ToArray()).ToListAsync();
            return View(emissionTelevisions);

        }

        public async Task<IActionResult> AfficherImage(int id)
        {
            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            Utilisateur user = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.UtilisateurId == id);
            if (user == null)
            {
                ModelState.AddModelError("", "Cet utilisateur n'existe pas.");
                return View();
            }

            ImageAffichageVM affichageVM = new ImageAffichageVM();
            affichageVM.NomUtil = user.Pseudo;
            affichageVM.ProfilePic = user.Photo == null ? null : $"data:image/png;base64, {Convert.ToBase64String(user.Photo)}";

            return View(affichageVM);
        }

        public IActionResult AjouterImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AjouterImage(ImageUploadVM iuvm)
        {
            if(_context.Utilisateurs == null)
            {
                return Problem("Il n'y a pas d'utilisateur.");
            }
            if (ModelState.IsValid)
            {
                Utilisateur? user = await _context.Utilisateurs.FirstOrDefaultAsync(x => x.Pseudo== iuvm.NomUtil);
                if (user == null)
                {
                    ModelState.AddModelError("NomUtil", "Ce user n'existe pas.");
                    return View();
                }
                if (iuvm.FormFile != null && iuvm.FormFile.Length >= 0)
                {
                    MemoryStream stream = new MemoryStream();
                    await iuvm.FormFile.CopyToAsync(stream);
                    byte[] photo = stream.ToArray();
                    user.Photo = photo;
                }

                await _context.SaveChangesAsync();
                return View();

            }
            ModelState.AddModelError("", "Il y a un problème avec le fichier fourni");
            return View();

        }
            // GET: EmissionTelevisions/Details/5
            public async Task<IActionResult> Details(int? id)
            {
            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            Utilisateur user = await _context.Utilisateurs.FindAsync(id);

            VM_UtilisateurMotDePasse vM_UtilisateurMotDePasse = new VM_UtilisateurMotDePasse()
            {
                Pseudo = user.Pseudo,
                NoTelephone = user.NoTelephone,
                PlateformeId = user.PlateformeId,
            };
            string query = "EXEC Personne.USP_Utilisateur_Dechiffrement @UtilisateurID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter{ParameterName = "@UtilisateurID", Value = id}
            };
            MotDePasse? motDePasse = (await _context.MotDePasses.FromSqlRaw(query, parameters.ToArray()).ToListAsync()).FirstOrDefault();
            if(motDePasse != null)
            {
                ViewData["mdP"] = motDePasse.MotDePasse1;
            }
            vM_UtilisateurMotDePasse.MotDePasseClair = motDePasse.MotDePasse1;
            return View(vM_UtilisateurMotDePasse);
        }

        // GET: EmissionTelevisions/Create
        public IActionResult Create()
        {
            ViewData["PlateformeId"] = new SelectList(_context.Plateformes, "PlateformeId", "PlateformeId");
            return View();
        }

        // POST: EmissionTelevisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VM_UtilisateurMotDePasse utilisateur)
        {
            bool existe = await _context.Utilisateurs.AnyAsync(x=>x.Pseudo == utilisateur.Pseudo);
            if (existe)
            {
                ModelState.AddModelError("Pseudo", "Ce pseudo est déjà utilisé.");
                return View(utilisateur);
            }

            string query = "EXEC Personne.USP_CreationUtilisateur_Chiffrement @Pseudo, @NoTelephone, @PlateformID, @MotDePasse";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter{ParameterName="@Pseudo", Value = utilisateur.Pseudo},
                new SqlParameter{ParameterName="@NoTelephone", Value = utilisateur.NoTelephone},
                new SqlParameter{ParameterName="@PlateformID", Value = utilisateur.PlateformeId},
                new SqlParameter{ParameterName="@MotDePasse", Value = utilisateur.MotDePasseHache},
            };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(query, parameters.ToArray());
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Une erreur est survenue. Veuillez réessayez.");
                return View(utilisateur);
            }
            return RedirectToAction("InfoUtilisateur");
        }

        // GET: EmissionTelevisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmissionTelevisions == null)
            {
                return NotFound();
            }

            var emissionTelevision = await _context.EmissionTelevisions.FindAsync(id);
            if (emissionTelevision == null)
            {
                return NotFound();
            }
            ViewData["PlateformeId"] = new SelectList(_context.Plateformes, "PlateformeId", "PlateformeId", emissionTelevision.PlateformeId);
            return View(emissionTelevision);
        }

        // POST: EmissionTelevisions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmissionTelevisionId,Nom,Descriptions,EstCoreen,Cote,NbVisionnement,CoutProduction,PlateformeId")] EmissionTelevision emissionTelevision)
        {
            /*if (id != emissionTelevision.EmissionTelevisionId)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emissionTelevision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmissionTelevisionExists(emissionTelevision.EmissionTelevisionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlateformeId"] = new SelectList(_context.Plateformes, "PlateformeId", "PlateformeId", emissionTelevision.PlateformeId);
            return View(emissionTelevision);
        }

        // GET: EmissionTelevisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmissionTelevisions == null)
            {
                return NotFound();
            }

            var emissionTelevision = await _context.EmissionTelevisions
                .Include(e => e.Plateforme)
                .FirstOrDefaultAsync(m => m.EmissionTelevisionId == id);
            if (emissionTelevision == null)
            {
                return NotFound();
            }

            return View(emissionTelevision);
        }

        // POST: EmissionTelevisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmissionTelevisions == null)
            {
                return Problem("Entity set 'ProjetFinal_2147037Context.EmissionTelevisions'  is null.");
            }
            var emissionTelevision = await _context.EmissionTelevisions.FindAsync(id);
            if (emissionTelevision != null)
            {
                _context.EmissionTelevisions.Remove(emissionTelevision);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmissionTelevisionExists(int id)
        {
          return (_context.EmissionTelevisions?.Any(e => e.EmissionTelevisionId == id)).GetValueOrDefault();
        }
    }
}
