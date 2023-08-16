// TODO
// better english distribution
// meaningful custom exception
// pull alphabet into class of its own
// multithreading

using System.Threading.Tasks;

namespace VigCrackerNS
{
    class VigCracker
    {
        // define charicteristics of alphabet
        // currently limited to contiguous spans of ASCII range
        public static readonly float[] ALPH_DIST = new float[] 
        {
            0.0812f, 
            0.0149f, 
            0.0271f, 
            0.0432f, 
            0.1200f, 
            0.0230f, 
            0.0203f, 
            0.0592f, 
            0.0731f, 
            0.0010f, 
            0.0069f, 
            0.0398f, 
            0.0260f, 
            0.0695f, 
            0.0768f, 
            0.0182f, 
            0.0011f, 
            0.0602f, 
            0.0628f, 
            0.0910f, 
            0.0288f, 
            0.0111f, 
            0.0209f, 
            0.0017f, 
            0.0210f, 
            0.0007f 
        };
        public static readonly char ALPH_MIN = 'A';
        public static readonly char ALPH_MAX = 'Z';

        static void Main(string[] args)
        {
            // SETUP
            // get command line args:
            // path to ciphertext
            // key range

            // SOLVER
            // Iterate over key lengths
            // break ciphertext into "key sets"
            // iterate over key sets for 26 shifts (25?)
            // find optimal shift all optimal shifts per key length
            // find optimal key length from consituent set shifts

            // TODO: remove, testing
            // int min_key_len = 3;
            // int max_key_len = 13;
            // string ciphertext = "this is a test";

            // List<SubSolver> solver_list = new();

            // // initialize a set of solvers for all potential key lenghts
            // for (int len = min_key_len; len <= max_key_len; len++)
            //     solver_list.Add(new(len, ciphertext));

            // foreach (var solver in solver_list)
            // {

            // }

            string test_cipher = "UYETIJBFTYIDWISCPLRDDUWLOFJTKFVLVJIIWXOPPEILRHVFHYXHHTWIMJAEUFVXMWFUUJSAJEXHHHFLVEHAQEHEFCSGVPBQIVMRHORPGZXTHEGKVXPYWPHEFXVOXORXUKLEFPFKFIWWKFFBUYIYFSCPTVHTKFBLUTLEVMSQUYIMIJHQPXITKFFPPKLAWUVBZNIRHOCQIZGKHSHEBESNHMCDBEHTKFHTPVRDVTHRDBSUWCSVPEHTKFBLUTLEVOSUUUEYSBPBHRRTKFKXMCWFUPABBTLSLESEFISLOFRRQRPOJBBAIVROWDVBEZXSHORPTFXHDUWQGZXTHERLXESVHSHEFVRDOPUPUYINKFFLMCIDXQZLHJJRRNHEFVRDVBBAOFXCKFRQIVQSRUVXUKLEBGWQUVHDRXBLWVVTKFGFEVPOJTBLXKLEZICIFYSUVFKXTKAOOPUPIZKHWISIPXWFLUHBEJSLLEZVUFKEWISOBKXHHDCOOVVSEVHKPCSGLTSSFITEUGSZUCCSWSOFHYXAQEOIMCSGVBFBCZKGHSOQPEIEQEHEBEETWISLUYIRHORPPTVAFLGTFIILHGHYFKAEHOHEFDELOBZLOXXHHXOIMJFUWUVXUUMDQPHJBKXEUCSZBLWESBKLVCHCKJBHUYSSHDFXDBWAOMPVIZQSHMTEFSYIOUHEFYSUVFHESVILRHGEJXLTKFBJBYILSFREJDTAOJTQFUSNHFBAPWELRHCKUFXHHXOIMKLEQNOEFCHIWXVFMVLEOJTQFUXHHPHEFIINGISPUFSDXQCKUYIWDMZQPTYTWISKPKGHHTOKEDEHHMDBEISLOBBAIFPDWISIPXAHLMSEFJITWMSAJKAHHSSFUJLOXMRYFKSMDLSQIVGOUOSOQVVFHDHIZJUUDSSPPCSGEZZLHKLEBCIFMKXHHXOIMJLIJISOUZPLWISVXVVESSSQUPLIJIOKECEUUBQLVCHNWHSQPMIRWISJBECMRSSPIVAAVUWOFUSFZBHZIZRGSBOKEDEBXJZAUYIHRVGBBEHSKFKBOKMNWPHEFKELOHFXTJIXSMCOJEKSXERBOCCSKFVBBIHPDTVLVKPEWHCDFKSUWGFLNLRDHSHEFSMGKFOSZCSGZBGPMZHIQHDXXRWTUZWKHKSHRMRRQYMSHORLGZXTRLSBQZXFUPACBCPIQHCKNRLEFPIIEEXIWDFXTYIDGPKKMRYRDTOTNRLUGEZBEFRTKFUOPLRDVISDPKXOPBOINFWTDTERJTOLBBGMBUMDSBYKFCXDRXBXOUGAOMSANRMNDEFBBUJUOWCFDVENGNODBJTEGJAXMCVIJIHQIVPOJXOPPELEUGCLUGELLGHBEKLEOPUXOUQASVZIFULEUGCLUWVOPVBAFIMTSBTBMKLEUUCPFVMFDOMYPEISZFFBCISKHOALWVCOXSOONJLEVBWAJJCOXSPXDBLUUUQXOPSUWVFKZFYRKFOANRQOYFREFIERPTOKEKYRQFREFILEDEHEBEOGREDXTRMDKFVBMGIDPBHLTZXUSTVBTRMDDHOFOZQAOMFFHYXCKBFIFJMTVKIPUDCFRPHNVZGKOZDXUFSKRGTEFIWHRFOKEJXOFLWKHYIFHMHEFIJORUOIMFZEUNCSJEKTKFOKLCIAQEHEFZRSWFDXOUIVHSMQPVHOHTWQILVTPVQEIVESNFRJBJJAFFKXTXVABBBAIVVMRVHEXRWAWJUEUCMNHOCQNLGHVISPBZH";

            // KeyFrag KFTest = new(test_cipher.ToList());

            // KFTest.Solve();

            // Console.WriteLine("Ciphertext: {0}\nDeviance: {1}\nShift: {2}\n", test_cipher, KFTest.GetDev(), KFTest.GetChar());

            // SubSolver SSTest = new(3, test_cipher);

            // SSTest.Eval();

            // Console.WriteLine("Key: {0}\n", SSTest.GetKey());

            Solver solver_test = new(3, 15, test_cipher);

            solver_test.Solve();
        }
    }

    class Solver
    {
        private int _min_key_len;
        private int _max_key_len;
        private string _ciphertext;
        private bool _silent;
        private float? _dev;
        private string? _key;

        public Solver(int min_key_len, int max_key_len, string ciphertext)
        {
            this._min_key_len = min_key_len;
            this._max_key_len = max_key_len;
            this._ciphertext = ciphertext;
        }

        public void Solve()
        {
            List<SubSolver> sub_solvers = new List<SubSolver>();

            // TODO scrutinize +1
            foreach (int key_len in Enumerable.Range(_min_key_len, (_max_key_len-_min_key_len+1)))
                sub_solvers.Add(new(key_len, _ciphertext));

            // spin up subsolvers in parallel for all possible key lengths in indicated range
            Parallel.ForEach(sub_solvers, subsolver => 
            {
                subsolver.Eval();
            });

            foreach (SubSolver ss in sub_solvers)
            {
                Console.WriteLine("Key: {0} Dev: {1}\n", ss.GetKey(), ss.GetDev());
            }
        }

        public void Report()
        {

        }
    }

    class SubSolver
    {
        // assuming correct key length is known, subsolver will compute optimal key

        private readonly string _ciphertext;
        private int _key_len;
        private string? _key;
        private float? _dev;

        public SubSolver(int key_len, string ciphertext)
        {
            this._key_len = key_len;
            this._ciphertext = ciphertext;
        }

        public float Eval()
        {
            // calculates optimal deviation from standard english letter distribution
            // AND populates key field with ideal key for given key length

            // KeyFrag could be tweaked to make this intermediary list step unnecessary. at present, underlying char list must be presented on construction,
            // so we have this nast array of lists
            // also, there has got to be a cleaner way to init a list of objects in a one liner lol
            List<char>[] key_sets = Enumerable.Range(0, _key_len).Select(_ => new List<char>()).ToArray();
            
            // break ciphertext into "key sets"
            for (int i = 0; i < _ciphertext.Length; i++)
                key_sets[i % _key_len].Add(_ciphertext[i]);

            KeyFrag[] key_frags = key_sets.Select(x => new KeyFrag(x)).ToArray();

            // find ideal "shift" for each key set and track cumulative deviance
            float cum_dev = 0;
            string construct_key = "";
            foreach (KeyFrag frag in key_frags)
            {
                frag.Solve();

                // TODO should I do something about the possible null here?
                // it will never happen, but the compiler doesn't know that I guess
                cum_dev += (float) frag.GetDev();
                construct_key += frag.GetChar();
            }

            _key = construct_key;
            _dev = cum_dev;

            // TODO maybe dont return here in keeping with rest of design...
            return cum_dev;
        }

        public string? GetKey()
        {
            return _key;
        }

        public float? GetDev()
        {
            return _dev;
        }
    }

    class KeyFrag
    {
        // given "key set", will compute optimal shift (and key fragment) to match english letter distribution
        // essentially solves a single Caesar cipher

        private float? _dev;
        private uint? _shift;
        // TODO private
        public List<char> _letters;

        public KeyFrag(List<char> letters)
        {
            this._letters = letters;
        }

        public void Solve()
        {
            // computes ideal shift, populating _dev and _shift
            // NOTE could introduce parallelism here, but opting to use it at the key length granularity
            foreach ( uint shift in Enumerable.Range(0, VigCracker.ALPH_DIST.Length))
            {
                // perform a shift
                List<char> cur_letters = _letters.Select(x => ShiftChar(x, shift)).ToList();

                // calculate deviation
                float curr_dev = CalcDev(cur_letters);

                // TODO testing remove
                // Console.WriteLine("shift: {0}, dev: {1}, plaintext:\n{2}\n\n", shift, curr_dev, new string(cur_letters.ToArray()));
                // TODO end testing

                // first iteration, or found more optimal shift
                if (_shift == null || curr_dev < _dev)
                {
                    _shift = shift;
                    _dev = curr_dev;
                }
            }
        }
        
        public char ShiftChar(char input, uint shift)
        {
            // shift a character by some amount within the given alphabet. wraps around on "overflow"

            uint letter_ascii = (uint) input;

            if (letter_ascii < (int) VigCracker.ALPH_MIN || letter_ascii > (int) VigCracker.ALPH_MAX)
                // TODO meaningful exception
                throw new Exception();

            letter_ascii -= (uint) VigCracker.ALPH_MIN;
            letter_ascii += shift;
            letter_ascii %= (uint) VigCracker.ALPH_DIST.Length;
            letter_ascii += (uint) VigCracker.ALPH_MIN;

            return (char) letter_ascii;
        }

        public float CalcDev(List<char> letters)
        {
            // calculates the deviation of a given set of characters from the standard alphabet distribution

            // calculate distribution
            float[] dist = Enumerable.Repeat(0.0f, VigCracker.ALPH_DIST.Length).ToArray();

            foreach (int letter_ascii in letters.Select(x => (int) x))
            {
                if (letter_ascii < (int) VigCracker.ALPH_MIN || letter_ascii > (int) VigCracker.ALPH_MAX)
                    // TODO meaningful exception
                    throw new Exception();

                // increase count of obsered letter
                dist[letter_ascii - VigCracker.ALPH_MIN] += 1.0f;
            }

            // normalize distribution
            float sum = 0;
            foreach (float f in dist)
                sum += f;

            for (int i = 0; i < VigCracker.ALPH_DIST.Length; i++)
                dist[i] /= sum;

            // calculate deviance from key distribution
            // curently using sum of absolute distance from the actual. could be tweeked, but im no statistician lol
            float sum_dev = 0.0f;
            for (int i = 0; i < VigCracker.ALPH_DIST.Length; i++)
                sum_dev += Math.Abs(VigCracker.ALPH_DIST[i] - dist[i]);

            return sum_dev;
        }

        public float? GetDev()
        {
            return _dev;
        }

        public char? GetChar()
        {
            // gets the character from the original "encryption" key based on the shift necessarry to "decrypt"
            if (_shift == null)
                // kinda weak tbh
                throw new Exception();

            // TODO SCRUTINIZE THIS
            // _shift is the amt needed to shift the ciphertext BACK into the plaintext (wrapping around), not the amt that the og ciphertext
            // was shifted by. that amt can be derived by the following calculation, but i am confused by the off by one error. I'll have
            // to think about it a little
            // edit: WHY did it take me so long to figure this out...
            return ShiftChar(VigCracker.ALPH_MAX, (uint) (VigCracker.ALPH_DIST.Length - _shift + 1));
        }
    }
}